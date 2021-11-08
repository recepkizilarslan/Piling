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

Piling supports is very easy to start port scan simple commands.

```sh
--scan {ipAddress or domain} --from {portStartRange} --to {portFinishRange} --save {OutputPath}
```

> Note: `all keywords` are required for port scanning.

Generate report for the spesific result

```sh
--scan {ipAddress or domain} --from {portStartRange} --to {portFinishRange} --save {OutputPath} --just {result type}
```

```sh
--scan google.com --from 12 --to 65535 --save "result.txt" --just opened
```

By a real example :

[Windows]

```sh
cd pilling
piling.exe --scan google.com --from 12 --to 65535 --save "result.txt" -just closed
```

[Linux]
```sh
cd bin/Debug/net5.0
./Piling --scan 127.0.0.1 --from 12 --to 65535 --save result.txt
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
## Start commands

| Command  | Description |
| ------------- | ------------- |
| --scan | Scan to target ip or domain |
| --from  | Port range start number  |
| --to  | Port range stop number  |
| --save  | Output file  |
| --just  | Report option  |

## State Action
You can change pause/ resume while scanning

| Command  | Description |
| ------------- | ------------- |
| ESC | Stop scanning  |
| Spacebar | Pause/Resume scanning  |


## License
MIT
