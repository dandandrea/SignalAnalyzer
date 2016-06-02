# Signal Analyzer
Signal analysis tool. Started developing this for figuring out some of the signals that I hear while scanning with my [Software Defined Radio (SDR)]. Right now there is just one method to read in a WAV file and return the predominant audio frequencies encountered in it. Uses [naudio] to read in the WAV file and perform an FFT to get the frequency components.

[Software Defined Radio (SDR)]: <https://www.amazon.com/RTL-SDR-Blog-RTL2832U-Software-Defined/dp/B0129EBDS2>
[naudio]: <https://github.com/naudio/NAudio>
