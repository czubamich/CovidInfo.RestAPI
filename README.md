# CovidInfo.RestAPI
<!-- PROJECT SHIELDS -->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]

<!-- PROJECT LOGO -->
<br />
  <p align="left">
    RESTful microservice for gathering and sharing COVID-19 related data in Poland. </br>
    API fetches data from "COVID-19 w Polsce" Google Spreadsheet created and maintained by Michał Rogalski
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#example-usage">Example usage</a></li>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#features">Features</a></li>
        <li><a href="#restapi-endpoints">RestAPI Endpoints</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#report-issues">Report Issues</a></li>
      </ul>
    </li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

### Example usage
API can be tested with following [Kotlin App](https://github.com/czubamich/CovidInfo.Kotlin)

### Built With

* [ASP.NET Web API](https://docs.microsoft.com/en-us/aspnet/web-api/)
* [Entity Framework 6](https://docs.microsoft.com/en-us/ef/ef6/)
* [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader)
* [Newtonsoft.Json](https://www.newtonsoft.com/json)

### Features
* Access to Covid-19 related data for country and regions through RestAPI
* Automatic fetching from COVID-19 w Polsce Google Spreadsheet
* Storing data on a SQL Server
* News channel

### RestAPI Endpoints
#### Get country list
```javascript
GET /api/country/list
```
#### Get regions list for selected country
```javascript
GET /api/region/{countryId}/list
```
#### Get all regions summary for today
```javascript
GET /api/region/{countryId}/summary
```
#### Get all entries for selected country/region
```javascript
GET /api/country/{countryId}
GET /api/region/{countryId}/{regionId}
```
#### Get latest single entry for selected country/region
```javascript
GET /api/country/{countryId}/today
GET /api/region/{countryId}/{regionId}/day/{date}
```
#### Get entry for selected day
```javascript
GET api/country/{countryId}/day/{date}
GET api/region/{countryId}/{regionId}/today
```
#### Get all entries since selected day
```javascript
GET api/country/{countryId}/since/{date}
GET api/region/{countryId}/{regionId}/since/{date}
```
#### Get number of news pages
```javascript
GET /api/country/{countryId}/news/pages
```
#### Get news from selected page (10 news per page)
```javascript
GET /api/country/{countryId}/news/{page}
```


<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites
To build this project, you require:

* Visual Studio 2019
* IIS Express

### Installation
1. Clone the repo
   ```sh
   git clone https://github.com/czubamich/CovidInfo.RestAPI.git
   ```
2. Open project in Vistual Studio.
3. Setup SQL database (PolandDatbaseQuery.sql)
4. Run 'app' F5.

### Report issues
If you find anything that is performing not as espected? Any feature that should be added or improved? Please feel free to check the issue tracked and create create a ticket. Please try to provide a detailed description of your problem, including the steps to reproduce it.

<!-- CONTACT -->
## Contact

Michael Czuba - czuba.mich@gmail.com

<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

* [COVID-19 w Polsce by Michał Rogalski](https://docs.google.com/spreadsheets/d/1ierEhD6gcq51HAm433knjnVwey4ZE5DCnu1bW7PRG3E/edit#gid=1309014089)

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/czubamich/CovidInfo.RestAPI.svg?style=for-the-badge
[contributors-url]: https://github.com/czubamich/CovidInfo.RestAPI/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/czubamich/CovidInfo.RestAPI.svg?style=for-the-badge
[forks-url]: https://github.com/czubamich/CovidInfo.RestAPI/network/members
[stars-shield]: https://img.shields.io/github/stars/czubamich/CovidInfo.RestAPI.svg?style=for-the-badge
[stars-url]: https://github.com/czubamich/CovidInfo.RestAPI/stargazers
[issues-shield]: https://img.shields.io/github/issues/czubamich/CovidInfo.RestAPI.svg?style=for-the-badge
[issues-url]: https://github.com/czubamich/CovidInfo.RestAPI/issues
[license-shield]: https://img.shields.io/github/license/czubamich/CovidInfo.RestAPI.svg?style=for-the-badge
[license-url]: https://github.com/czubamich/CovidInfo.RestAPI/blob/master/LICENSE.txt

<!-- README created using the following template -->
<!-- https://github.com/othneildrew/Best-README-Template -->
