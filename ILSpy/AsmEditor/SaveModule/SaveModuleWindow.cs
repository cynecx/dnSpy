﻿/*
    Copyright (C) 2014-2015 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel;
using System.Windows;

namespace ICSharpCode.ILSpy.AsmEditor.SaveModule
{
	public class SaveModuleWindow : WindowBase
	{
		public SaveModuleWindow()
		{
			Loaded += SaveMultiModule_Loaded;
		}

		void SaveMultiModule_Loaded(object sender, RoutedEventArgs e)
		{
			var data = (SaveMultiModuleVM)DataContext;
			data.OnSavedEvent += SaveMultiModuleVM_OnSavedEvent;
		}

		void SaveMultiModuleVM_OnSavedEvent(object sender, EventArgs e)
		{
			var data = (SaveMultiModuleVM)DataContext;
			if (!data.HasError)
				okButton_Click(null, null);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			var data = (SaveMultiModuleVM)DataContext;
			if (data.IsSaving) {
				var res = MainWindow.Instance.ShowMessageBox("Are you sure you want to cancel the save?", MessageBoxButton.YesNo);
				if (res == MsgBoxButton.OK)
					data.CancelSave();
				e.Cancel = true;
				return;
			}

			if (data.IsCanceling) {
				var res = MainWindow.Instance.ShowMessageBox("The save is being canceled.\nAre you sure you want to close the window?", MessageBoxButton.YesNo);
				if (res != MsgBoxButton.OK)
					e.Cancel = true;
				return;
			}
		}

		internal void ShowOptions(SaveModuleOptionsVM data)
		{
			if (data == null)
				return;

			var win = new SaveModuleOptions();
			win.Owner = this;
			var clone = data.Clone();
			win.DataContext = clone;
			var res = win.ShowDialog();
			if (res == true) {
				clone.CopyTo(data);
				((SaveMultiModuleVM)DataContext).OnModuleSettingsSaved();
			}
		}
	}
}
