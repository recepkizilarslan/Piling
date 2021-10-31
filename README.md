# Piling Smart Port Scanner

Pilling is a multi threaded port scanner tool. Write command such as plain text and get a result supported output type (csv,txt).

## Development

Want to contribute? Great!
Piling uses .Net 5.0 for fast developing. Make a change in your file and instantaneously see your updates!

Open your favorite Terminal and run these commands.

Installation:
```sh
git clone https://github.com/recepkizilarslan/Piling
```

And:

```sh
dotnet restore
```

Build:

```sh
dotnet build
```

## Usage

Piling supports is very easy to start port scan by plain syntax.

```sh
scan {ipAddress} from {portStartRange} to {portFinishRange} then save {OutputPath}
```

> Note: `all keywords` are required for port scanning.

By a real example :

```sh
cd pilling
piling.exe scan 127.0.0.1 from 12 to 65535 then save "C:\Users\RecepKizilarslan\Documents\result.txt"
```
This will start scanning to 127.0.0.1 and analyze opened ports and save identified output.

```sh
127.0.0.1 : 137 - Closed - 10/31/2021 13:53:43 - 63
127.0.0.1 : 135 - Open - 10/31/2021 13:53:43 - 20
127.0.0.1 : 43 - Closed - 10/31/2021 13:53:45 - 2415
127.0.0.1 : 42 - Closed - 10/31/2021 13:53:45 - 2421
127.0.0.1 : 28 - Closed - 10/31/2021 13:53:45 - 2432
127.0.0.1 : 19 - Closed - 10/31/2021 13:53:45 - 2584
127.0.0.1 : 17 - Closed - 10/31/2021 13:53:45 - 2584
127.0.0.1 : 25 - Closed - 10/31/2021 13:53:45 - 2606
127.0.0.1 : 62 - Closed - 10/31/2021 13:53:45 - 2461
```

## License
MIT
