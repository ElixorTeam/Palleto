﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;

namespace ScalesUI.Utils;

internal static class ActionUtils
{
	#region Public and private fields, properties, constructor

	private static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	private static UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;

	#endregion

	#region Public and private methods

	internal static void MakeScreenShot()
	{
		using MemoryStream memoryStream = new();

		Rectangle bounds = Screen.GetBounds(Point.Empty);
		using Bitmap bitmap = new(bounds.Width, bounds.Height);
		using Graphics graphics = Graphics.FromImage(bitmap);
		graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
		Image img = bitmap;
		img.Save(memoryStream, ImageFormat.Png);

		ScaleScreenShotModel scaleScreenShot = new() { Scale = UserSession.Scale, ScreenShot = memoryStream.ToArray() };
		DataAccess.Save(scaleScreenShot);
	}

	private static void MakeScreenShot(IWin32Window win32Window)
	{
		using MemoryStream memoryStream = new();

		if (win32Window is Form form)
		{
			using Bitmap bitmap = new(form.Width, form.Height);
			using Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(form.Location.X, form.Location.Y, 0, 0, form.Size);
			using Image img = bitmap;
			img.Save(memoryStream, ImageFormat.Png);
		}

		ScaleScreenShotModel scaleScreenShot = new() { Scale = UserSession.Scale, ScreenShot = memoryStream.ToArray() };
		DataAccess.Save(scaleScreenShot);
	}

	internal static void ActionTryCatchFinally(IWin32Window win32Window, Action action, Action actionFinally)
	{
		try
		{
			action.Invoke();
		}
		catch (Exception ex)
		{
			ActionMakeScreenShot(win32Window);
			GuiUtils.WpfForm.CatchException(ex, win32Window, true, true, true);
		}
		finally
		{
			actionFinally.Invoke();
		}
	}

	internal static void ActionTryCatch(IWin32Window win32Window, Action action)
	{
		try
		{
			action.Invoke();
		}
		catch (Exception ex)
		{
			ActionMakeScreenShot(win32Window);
			GuiUtils.WpfForm.CatchException(ex, win32Window, true, true, true);
		}
	}

	internal static void ActionMakeScreenShot(IWin32Window win32Window)
	{
		try
		{
			MakeScreenShot(win32Window);
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(ex, win32Window, true, true, true);
		}
	}

	#endregion
}