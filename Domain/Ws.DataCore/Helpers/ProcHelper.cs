namespace Ws.DataCore.Helpers;

public sealed class ProcHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static ProcHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static ProcHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public methods

	public void Run(string procName, string args, bool runAs, ProcessWindowStyle windowStyle, bool useShellExecute)
	{
		Process process = new()
		{
			StartInfo = new(procName, args)
			{
				Verb = runAs ? "runas" : "",
				WindowStyle = windowStyle,
				UseShellExecute = useShellExecute
            }
        };
		process.Start();
		process.WaitForExit();
	}

	#endregion
}