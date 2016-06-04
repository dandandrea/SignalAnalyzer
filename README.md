# Signal Analyzer
Started developing this for figuring out some of the signals that I hear while scanning with my [Software Defined Radio (SDR)]. Right now there is just one method to read in a WAV file and return the predominant audio frequencies encountered in it. Uses [Naudio library] to read in the WAV file and [Math.NET library] to perform an FFT to get the frequency components.

[Software Defined Radio (SDR)]: <https://www.amazon.com/RTL-SDR-Blog-RTL2832U-Software-Defined/dp/B0129EBDS2>
[NAudio library]: <https://github.com/naudio/NAudio>
[Math.NET library]: <https://mathdotnet.com>
