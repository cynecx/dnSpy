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
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ICSharpCode.ILSpy.dntheme
{
	sealed class FileTabBackgroundConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string rsrcName;
			switch ((TabManagerState)value) {
			case TabManagerState.Empty:		rsrcName = "TransparentBrush"; break;
			case TabManagerState.Active:	rsrcName = "EnvironmentFileTabSelectedGradient"; break;
			case TabManagerState.Inactive:	rsrcName = "EnvironmentFileTabInactiveGradient"; break;
			default: throw new InvalidOperationException();
			}
			return (Brush)App.Current.FindResource(rsrcName);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
