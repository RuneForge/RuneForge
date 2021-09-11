# Contributing to RuneForge

Please read this document to the end before contributing.

## How to contribute

### Branching model

We use the Gitflow workflow design to keep the branch structure clear and easy to maintain.

[Here](https://www.atlassian.com/git/tutorials) is a tutorial that will help you learn more about Git, including the Gitflow workflow.

In the repository we have two long-running branches: a `main` branch for stable versions and a `development` branch for everyday developing.

However, there are also short-living branches of the following types:

- **Feature branches.** These are the branches where user stories should be implemented.  
  Prefix: `feature/`  
  Created from a commit on: `development`  
  Merged into: `development`  

- **Release branches.** When all milestones of an incoming release are reached and required features are implemented, a release branch should be created.  
  Prefix: `release/`  
  Created from a commit on: `development`  
  Merged into: `main`, `development`  

- **Hotfix branches.** If there is a critical bug that persists on the `main` branch, it should be fixed immediately in a hotfix branch.  
  Prefix: `hotfix/`  
  Created from a commit on: `main`  
  Merged into: `main`, `development`  

Please notice that we merge branches only via merge commits, not using the fast-forward merge or rebase.

### Submitting changes

If you have some features or fixes you want to share with us, you should create a separate pull request for each feature. We will discuss your changes and merge them when they are ready.

### Names of pull requests and issues

To make it easier to maintain issues and pull requests, please name them in the following style:

`[Category/Subcategory] Description`

Examples:

> [Entities/Units] The unit's location is not saved when leaving the game

> [Core/Graphics] The unit's texture is drawn without the player's color

Also please use the same convention for the commit messages when merging branches and pull requests:

> [Entities/Units] Implement saving of a unit's location on leaving

If you have a problem with picking an appropriate category for an issue/pull request, the namespace of the type associated with the problem might be the right solution.

## Code style

We are following these conventions, guidelines and patterns:

* [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)
* [Naming Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)
* [Type Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/type)
* [Member Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/member)
* [Designing for Extensibility](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/designing-for-extensibility)
* [Design Guidelines for Exceptions](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/exceptions)
* [Usage guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/usage-guidelines)
* [Common Design Patterns](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/common-design-patterns)

In addition, there is a `.editorconfig` file in the root folder of the repository so your text editor can be configured to follow our code style automatically.
