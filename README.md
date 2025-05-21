# aras-innovator-cli

A sample command-line interface (CLI) project for interacting with Aras Innovator.

**aras-innovator-cli** is a Visual Studio solution that demonstrates one approach to building standalone executable tools for Aras Innovator. This sample .NET Framework Console Application uses the [Aras.IOM SDK](https://www.nuget.org/packages/Aras.IOM) to connect to an Aras server and execute AML scripts.

## Project Details

Release | Notes
--------|--------
[v1.1.0](https://github.com/ArasLabs/aras-innovator-cli/releases/tag/v1.1.0) | Project was upgraded to use the standalone [Aras.IOM SDK](https://www.nuget.org/packages/Aras.IOM) via Nuget.org. This change ensures the built executable can be used with any Aras Innovator release supported by the SDK. Previously, the .exe was only sure to work with the Aras Innovator release the project was built for.
[v1.0.0](https://github.com/ArasLabs/aras-innovator-cli/releases/tag/v1.0.0) | Sample project demonstrating how to create a CLI program that uses IOM to communicate with Aras Innovator.

### Compatibility

Project | Aras Innovator
--------|------
[v1.1.0](https://github.com/ArasLabs/aras-innovator-cli/releases/tag/v1.1.0) | 12 SP18, R14-R35+
[v1.0.0](https://github.com/ArasLabs/aras-innovator-cli/releases/tag/v1.0.0) | 11 SP12

## Installation

This project provides sample code for a standalone CLI tool that can be executed via Command Prompt/terminal or other application. It does not need to be installed. However, the Visual Studio solution does need to be built to create the .exe.

The following section describes how to build the Visual Studio solution.

### Prerequisites

- Visual Studio 2022 (Community, Professional, or Enterprise)
- .NET Framework 4.7
- Access to Aras Innovator server (for testing)

### Build Instructions

1. Clone or download this repository to your local machine.
2. Open the solution file (`aras-innovator-cli.sln`) in Visual Studio 2022.
3. Restore NuGet packages if prompted.
4. Build the solution by selecting **Build > Build Solution** from the menu or pressing `Ctrl+Shift+B`.
5. The executable will be located in the `bin\Debug` or `bin\Release` folder, depending on your build configuration.

## Usage

![CLI interface](screenshots/innovatorCLI.PNG)

After [building the solution](#build-instructions), you can run the CLI executable from the command line. The tool requires several mandatory arguments and supports optional arguments for output and logging.

### Command

```bash
aras-innovator-cli.exe -l <url> -d <dbname> -u <user> -p <password> -f <input_aml_file> [options]
```

### Mandatory Arguments

| Argument | Description            | Example                |
|----------|------------------------|------------------------|
| `-l`     | Aras Innovator URL     | `-l http://localhost/InnovatorServer` |
| `-d`     | Database name          | `-d InnovatorSolutions`|
| `-u`     | User login             | `-u admin`             |
| `-p`     | User password          | `-p innovator`         |
| `-f`     | Input AML file path    | `-f C:\input.xml`      |

### Optional Arguments

| Argument | Description            | Example                |
|----------|------------------------|------------------------|
| `-o`     | Output file path       | `-o C:\output.xml`     |
| `-g`     | Log file path          | `-g C:\log.txt`        |

### Example

```bash
aras-innovator-cli.exe -l http://localhost/InnovatorServer -d InnovatorSolutions -u admin -p innovator -f C:\input.xml -o C:\output.xml -g C:\log.txt
```

This command sends the AML content from `input.xml` to the specified Aras Innovator server and writes the response to `output.xml` and logs to `log.txt`.

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

For more information on contributing to this project, another Aras Labs project, or any Aras Community project, shoot us an email at [araslabs@aras.com](mailto:araslabs@aras.com).

## Credits

### Creator

Project written, documented, and published by Yoann Maingon at Aras Labs. @YoannArasLab

### Contributors

[Eli J. Donahue](https://github.com/EliJDonahue), Aras Labs

## License

Aras Labs projects are published to Github under the MIT license. See the [LICENSE file](https://github.com/ArasLabs/toc-search-bar/blob/master/LICENSE.md) for license rights and limitations.
