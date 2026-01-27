# Project Overview

- This project is a Selenium-based automation testing framework for testing various modules of the cloud test portal.
- It is built using C# with the MSTest testing framework.

# Files and Directories

- Modules/ - Holds all test classes (each module has a separate test class).
- utils/ - Contains helper class for pagination, setupdata, export results to excel.
- utils/data - Stores datas used for data-driven testing.

# Setup Project

- clone project from git
- go to the directory double click on **VMS-Phase1PortalAT.sln** file, project will open in VSCode
- click on build/build solution.
- open test explorer where all test cases will displayed
- go to setupData.cs file and update the path as you need

# Before Run Test Case

TODO:change datas in file inside **utils/data** used in respective test case

# Run All Tests

To run all tests, open Test Explorer in Visual Studio and click on Run All.

# Run Specific Test

You can also run individual tests by right-clicking on the test method in Visual Studio and selecting Run.

# Generating Test Reports

Each test results will get stored in excel where path is specified in setupData.cs

# Release Notes

## v1.0.0

### Modules Covered

- Machines
- Transactions
- Products
- Warehouse
- Company

### Features Covered

- add
- edit
- list
- search
- sort
- data range filters
- other actions
- pagination
- export
- import
