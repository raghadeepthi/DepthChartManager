# About

Most team sports have a depth chart (a ranking of each player) for each position they have. 

For example in NFL:

Ben Roethlisberger is listed as the starting QB and first on the QB depth chart.

Landry Jones, his backup is listed as the 2nd person on that depth chart.

This project tries to implement the depth chart functionality.

# Design

The solution is designed with extensibility in mind.

Following layers helps maintaining code complexity:

## Domain

This is the heart of application. 

Domain have been clearly defined. Instad of making POCO classes, I have tried to make them rich with functionality.

## Core

This is an application/core layer where various interfaces (repositories/services) are declared. The only dependency this layaer has is Domain layer.

## Infrastructure

This is an infrastructure layer which implements outer layer concerns like repository.

## Console App

As the name suggests its the console app that runs the NFL scenario. This layer uses IDepthChartManager service to manage depth chart.

With service layer being totally independent of the console app, it can be easily plugged into web api framework too without any code change.

# Development

## Software Prerequisites

|Software|Mandatory|Download Link
|---|---|---|
|.NET Core 5.0.302|Yes|https://dotnet.microsoft.com/download|

## Build

`dotnet build`

## Test

`dotnet test`

## Run

`dotnet run --project DepthChartManager.ConsoleApp`
