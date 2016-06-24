x
# Signal Analyzer
Software for analyzing [digital radio signals].

Contains classes and methods for analyzing WAV recordings of digital radio signals, specifically those which use [FSK encoding]. Also includes classes and methods for generating digital radio signals.

Includes both a GUI and Command-Line Interface (CLI) for interacting with the core library.

![Screenshot](http://dandandrea.github.com/screenshot.png)

Eventual goals include automatic determination of FSK mark and space frequencies and [baud rate] and then using this information to extract the binary data contained in a signal sample.

Uses [Naudio library] to read in a WAV file, [Math.NET Numerics library] to perform FFTs to get its frequency components, and [accord-net library] for its [k-means clustering] implementation.

Get started with your own digital radio signal analysis using this software and a [Software Defined Radio (SDR)].

[baud rate]: <https://en.wikipedia.org/wiki/Baud>
[digital radio signals]: <http://www.kb9ukd.com/digital/>
[k-means clustering]: <https://en.wikipedia.org/wiki/K-means_clusterin://en.wikipedia.org/wiki/K-means_clustering>
[Software Defined Radio (SDR)]: <https://www.amazon.com/RTL-SDR-Blog-RTL2832U-Software-Defined/dp/B0129EBDS2>
[FSK encoding]: <https://en.wikipedia.org/wiki/Frequency-shift_keying>
[NAudio library]: <https://github.com/naudio/NAudio>
[accord-net library]: <https://github.com/accord-net/framework>
[Math.NET Numerics library]: <https://github.com/mathnet/mathnet-numerics>
