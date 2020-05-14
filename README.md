<img src="https://github.com/dutch-and-bold/moneybird-cli/raw/master/.github/moneybird-cli-logo.png" alt="Moneybird CLI Project Logo" title="Moneybird" align="right" height="64" srcset="https://github.com/dutch-and-bold/moneybird-cli/raw/master/.github/moneybird-cli-logo@2x.png 2x"/>
 
 # Moneybird CLI
 
 ![.NET Core](https://img.shields.io/badge/.NET%20Core-3.1-purple)

Moneybird CLI tool. This is currently a tech demo for the [Moneybird SDK](https://github.com/dutch-and-bold/moneybird-sdk) project.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Project structure](#project-structure)
- [Support](#support)
- [Contributing](#contributing)

## Installation

1. Clone repository
2. Configure environment variables as described below
3. Login using `dotnet run --project src/DutchAndBold/MoneybirdCli login`

| Environment variable                      | Description                                            |
| ----------------------------------------- | ------------------------------------------------------ |
| MoneyBirdCli_Moneybird__Api__ClientId     | The client id obtained with the instructions below     |
| MoneyBirdCli_Moneybird__Api__ClientSecret | The client secret obtained with the instructions below |

**Requirements**
* Dotnet SDK >= 3.1 **

** Because this currently is a tech demo, it requires compilation with the Dotnet SDK. Pre-compiled binaries will become available later.

### Getting a client id and secret

1. Go to https://moneybird.com/user/applications/new
2. Choose 'API Token for external application'
3. Enter `urn:ietf:wg:oauth:2.0:oob' as redirect url.

## Usage

Test if connection is working using `dotnet run --project src/DutchAndBold/MoneybirdCli test-connection`

## Support

Please [open an issue](https://github.com/dutch-and-bold/moneybird-sdk/issues/new) for support.

## Contributing

Please contribute using [Github Flow](https://guides.github.com/introduction/flow/). Create a branch, add commits, and [open a pull request](https://github.com/dutch-and-bold/moneybird-sdk/compare/).

### Setting up the project

1. Clone the project to a local directory
2. Restore packages with nuget

**Requirements**
* Dotnet SDK >= 3.1

### Coding style and rules

This project adopts the Microsoft recommended code quality rules and .NET API usage rules. To adhere to these rules the project uses [Microsoft.CodeAnalysis.FxCopAnalyzers](https://www.nuget.org/packages/Microsoft.CodeAnalysis.FxCopAnalyzers/) package for code analysis in all projects.