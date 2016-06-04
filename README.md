# Signal Analyzer
Started developing this for analyzing signals heard while listening to a [Software Defined Radio (SDR)]. Right now there is just one method to read in a WAV file and return the predominant frequency components encountered within it, which should be useful for analyzing [FSK-encoded signals]. Uses [Naudio library] to read in the WAV file and [Math.NET Numerics library] to perform an FFT to get the frequency components.

[Software Defined Radio (SDR)]: <https://www.amazon.com/RTL-SDR-Blog-RTL2832U-Software-Defined/dp/B0129EBDS2>
[FSK-encoded signals]: <https://en.wikipedia.org/wiki/Frequency-shift_keying>
[NAudio library]: <https://github.com/naudio/NAudio>
[Math.NET Numerics library]: <https://github.com/mathnet/mathnet-numerics>
