# Hikari

A custom spaced repetition system built with ASP.NET Core and React.

## What is Hikari?

Hikari is a spaced repetition application designed for personal learning and knowledge retention. Unlike existing solutions, this system is built from the ground up with modern web technologies and a focus on user experience.

**Current Status**: Phase 1 - Planning & Architecture

## Why Build This?

Read about [why I'm building this in the open](APPROACH.md) for the full philosophy and approach.

## Architecture

- **Backend**: ASP.NET Core Web API
- **Frontend**: React with TypeScript
- **Database**: PostgreSQL with Entity Framework Core
- **Authentication**: Microsoft Identity for ASP.NET Core
- **Testing**: xUnit with Shouldly assertions, NSubstitute mocking, Testcontainers for integration tests
- **Hosting**: Azure

## Commit Conventions

This project uses [Conventional Commits](https://www.conventionalcommits.org/) with custom types:

- `feat`: New features
- `fix`: Bug fixes
- `arch`: Architectural decisions and design choices
- `infra`: Infrastructure, tooling, CI/CD setup
- `docs`: Documentation updates
- `test`: Testing additions
- `refactor`: Code improvements without functional changes

Use the commit body to explain architectural reasoning and design decisions.

## Getting Started

*Coming soon - project is in early planning phase*

## Project Structure

```
/src
  /Hikari.Api/          # ASP.NET Core Web API
  /Hikari.Web/          # React frontend
/tests/                 # Test projects
/docs/                  # Technical documentation
```

---

*This project is built in public. Follow along with the development process and architectural decisions through commit history and documentation.*
