![Human Made](https://img.shields.io/badge/content-human--made-brightgreen?logo=github) 

*All files in this repository are created by humans. AI tools may be used for research, but no AI-generated content is included*

---

Modern games are no longer applications.
They are ecosystems.

A single project often consists of gameplay code, asset pipelines, editors, content processors, build tools, package registries, plugins, deployment targets and runtime modules. Yet most development environments still treat these as independent systems connected through scripts, configuration files and custom tooling.

Hamper was built around a different idea:
> Instead of connecting tools together, it connects knowledge

Projects describe themselves through their structure. Packages expose capabilities. Tools contribute context. [The framework](https://raw.githubusercontent.com/SchroedingerEntertainment/Publications/main/SDK%202.0%20-%20Capability-Oriented%20Software%20Architecture.pdf) aggregates that information into a shared understanding of the project and uses it to discover, compose and automate every stage of development.

Everything else follows naturally

---

## Built for Game Development

**Hamper is designed specifically for game development.**

Although many of its ideas could be applied elsewhere, the framework is intentionally optimized for games, [game engines](https://raw.githubusercontent.com/SchroedingerEntertainment/Publications/main/A%20Modern%20View%20on%20Game%20Engine%20Architecture.pdf) and [development tools](https://raw.githubusercontent.com/SchroedingerEntertainment/Publications/main/Data%20Driven%20Development%20Environment.pdf) – not traditional business applications, web services or database systems.

Game development has unique requirements:

- Projects evolve continuously
- Editors become part of the production pipeline
- Assets generate source code
- Build systems produce runtime data
- Tools need access to the same project information as the game itself

Rather than treating these as separate domains, Hamper models them as different perspectives of the same workspace. The framework becomes the common language shared by the engine, the tools and the build system

---

## Software Should Explain Itself

One of Hamper's fundamental principles is simple:

> Software already contains most of the information required to understand itself.


Folder structures, Naming conventions, Package layouts, Dependencies, Language-specific project structures. All of these describe intent.


Instead of asking developers to duplicate that information in configuration files, Hamper continuously discovers, aggregates and enriches the available context.

Configuration still exists.
But configuration should describe exceptions – not reality

---

## Context First

Traditional frameworks begin with configuration, Hamper begins with context.
Every project starts as a collection of observable facts:

```
Workspace
    │
    ▼
Discovery
    │
    ▼
Packages
    │
    ▼
Capabilities
    │
    ▼
Composition Graph
    │
    ▼
Build & Runtime
```

Each processing stage contributes new information without replacing previous knowledge. The result is a continuously evolving project graph that becomes more accurate over time.

Every subsystem inside Hamper works from the same context

---

## Capability-Oriented Composition


Applications are not assembled through dependencies. They are composed through capabilities.

A module never needs compile-time knowledge of another module. Instead, it declares the capabilities it provides. Consumers describe the capabilities they require.

During composition, Hamper discovers every available provider, resolves compatible capabilities and assembles the application automatically. Hosts and modules however remain independent.

This allows projects to grow without introducing unnecessary coupling between components

---

## Data Drives Everything

Hamper treats [software as data](https://raw.githubusercontent.com/SchroedingerEntertainment/Publications/main/Sparse%20based%20ECS.pdf).

Projects are represented as entities, metadata becomes components, systems aggregate information and pipelines transform context. Rather than executing imperative build scripts, Hamper continuously enriches the project graph until information exists to perform the requested operation.

Projects are discovered from a workspace. Packages and projects transform into modules exposing and consuming capabilities. Modules are then assembled into applications

---

## Convention over Configuration

Hamper follows a strict Convention over Configuration philosophy.

If a project already communicates its intent through structure, the framework should understand it automatically.

Common directory layouts, package descriptors, language and naming conventions. All of these become the primary source of truth.

Explicit configuration is only required when intent cannot be derived automatically or when a project intentionally deviates from established conventions.

This keeps projects easier to understand, easier to maintain and significantly reduces configuration overhead

---

## One Framework

Hamper includes [a build system](https://raw.githubusercontent.com/SchroedingerEntertainment/Publications/main/Data-Driven%20Build%20System.pdf), a package manager and development tools.

They are different views of the same project graph. The build system uses the same context as the runtime. The runtime uses the same package model as the editor. The editor uses the same capability graph as the package manager.

Every part of the framework builds upon the same foundation

---

## Philosophy

Hamper is built around a simple belief:

> Developers should spend their time building games – not infrastructure.

Projects already know far more about themselves than traditional development environments assume. The framework's responsibility is not to ask developers to describe their project repeatedly.
Its responsibility is to discover it.
Aggregate it.
Compose it.

And make that knowledge available wherever it is needed

---

## Contributing

Contributions should be made through a fork of the development branch.

Before submitting a pull request:

- ensure the change compiles, is complete and tested
- provide a clear description of the modification
- explain the motivation and expected impact of the change

Pull requests are reviewed by the project maintainers. Changes require approval from a maintainer before being merged.
A contribution must comply to the legal terms and conditions of this project.

A change missing the [Contribution Relicensing Exception](LICENSE.exception) might be rejected

---

## License

This project is licensed under the terms of the GNU Affero General Public License v3.0.

Permissions of this license are conditioned on making available complete source code of licensed works and modifications, which include larger works using a licensed work, under the same license. Copyright and license notices must be preserved. Contributors provide an express grant of patent rights. When a modified version is used to provide a service over a network, the complete source code of the modified version must be made available.

By contributing to this repository, you agree that your contributions are provided under the same license terms. Additional permissions and exceptions may apply as described in the project license and exception documents
