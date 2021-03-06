﻿
using HideVolumeOSD.Properties;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace HideVolumeOSD
{
	/// <summary>
	/// 
	/// </summary>
	static class Program
	{
		public static bool InitFailed = false;

		static Mutex mutex = new Mutex(true, "{CBF79D66-07FF-4B5E-9A48-94E85A139D68}");

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (mutex.WaitOne(TimeSpan.Zero, true))
			{
				if ((args.GetLength(0) == 1))
				{
					HideVolumeOSDLib lib = new HideVolumeOSDLib(null);

					lib.Init();

					if (args[0] == "-hide")
					{
						lib.HideOSD();
					}
					else
						if (args[0] == "-show")
						{
							lib.ShowOSD();
						}
				}
				else
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);

					using (ProcessIcon pi = new ProcessIcon())
					{
						pi.Display();

						if (!InitFailed)
							Application.Run();
					}
				}

				mutex.ReleaseMutex();
			}
		}
	}
}