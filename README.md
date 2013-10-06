# GitHub Push Lib

[![Build Status](https://travis-ci.org/pseudomuto/githubpushlib.png?branch=master)](https://travis-ci.org/pseudomuto/githubpushlib)

A simple library to programmatically push files to GitHub. The idea is to specify a file (or 
embedded resource) and have that file pushed to a specific repo at a specified path.

## Installation

The project is distributed as a NuGet Package...

    Install-Package GitHubPushLib

## Usage

    var service = new ContentService(token);
    var file = new DiskFile("Files/content_file.gif");
    var target = new FileTarget(owner, repo, file.Name);

    service.PushFile(file, target, "pushing file via GitHubPushLib");

For a working demo, check out the [Sample App]...

[Sample App]: https://github.com/pseudomuto/githubpushlib/tree/master/src/SampleApp
