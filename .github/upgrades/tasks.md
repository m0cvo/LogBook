# LogBook ASP.NET Core net10.0 Upgrade Tasks

## Overview

This document tracks the atomic upgrade of the `LogBook` solution to .NET `net10.0`, applying project and package updates in a single coordinated pass followed by validation. Prerequisites are verified first, then the atomic upgrade and testing are executed, with a final commit at the end.

**Progress**: 1/4 tasks complete (25%) ![0%](https://progress-bar.xyz/25)

---

## Tasks

### [✓] TASK-001: Verify prerequisites (SDK and configuration) *(Completed: 2026-01-28 11:04)*
**References**: Plan §Phase 0, Plan §Project-by-Project Plans

- [✓] (1) Verify required .NET 10 SDK is installed on the execution environment per Plan §Phase 0
- [✓] (2) Runtime/SDK version meets minimum requirements (**Verify**)
- [✓] (3) If present, verify `global.json` is compatible with the required SDK per Plan §Project-by-Project Plans
- [✓] (4) `global.json` (if present) is compatible with target SDK (**Verify**)

---

### [▶] TASK-002: Atomic framework and package upgrade with compilation fixes
**References**: Plan §Phase 1 (Atomic Upgrade), Plan §Project-by-Project Plans, Plan §Package Update Reference, Plan §Breaking Changes Catalog

- [✓] (1) Update project file properties per Plan §Project-by-Project Plans (apply `TargetFramework` updates to `LogBook\LogBook.csproj` as specified)
- [▶] (2) Apply package updates per Plan §Package Update Reference (only if package updates are chosen in the plan) and ensure `Directory.Build.*` or imported MSBuild files are updated in the same pass if required
- [ ] (3) Restore dependencies for `LogBook.sln` (e.g., `dotnet restore`) and ensure restore completes successfully (**Verify**)
- [▶] (4) Build the solution and fix all compilation errors caused by the framework/package changes per Plan §Breaking Changes Catalog
- [ ] (5) Solution builds with 0 errors (**Verify**)

---

### [ ] TASK-003: Run test suite and validate upgrade
**References**: Plan §Phase 2 (Validation), Plan §Testing Strategy, Plan §Breaking Changes Catalog

- [ ] (1) Run all automated test projects referenced by the solution per Plan §Testing Strategy (if test projects exist)
- [ ] (2) Fix any test failures discovered (reference Plan §Breaking Changes Catalog for common issues)
- [ ] (3) Re-run tests after fixes
- [ ] (4) All tests pass with 0 failures (**Verify**)

---

### [ ] TASK-004: Final commit of upgrade changes
**References**: Plan §Source Control Strategy

- [ ] (1) Commit all remaining changes with message: "TASK-004: Complete upgrade of LogBook to net10.0"





