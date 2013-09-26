# GitHub PushLib Sample Application

This application demonstrates how to use the GiHub PushLib library.

## Setup

In order to run the application, it needs to know your GitHub OAuth token. It is bad form 
to include this information in source files, so the application will look for an environment 
variable called `GHPL_TOKEN` and use it's value.

On Windows machines, open a command prompt (as an Administrator) and run the following:

	setx GHPL_TOKEN <your_auth_token>

Other options like which repo to work with are set in `App.config`.