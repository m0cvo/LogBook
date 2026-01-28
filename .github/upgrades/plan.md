# .github/upgrades/plan.md

Table of contents
- Executive Summary
- Migration Strategy
- Detailed Dependency Analysis
- Project-by-Project Plans
- Package Update Reference
- Breaking Changes Catalog
- Testing Strategy
- Risk Management
- Complexity & Effort Assessment
- Source Control Strategy
- Success Criteria

---

## Executive Summary

Selected Strategy
**All-At-Once Strategy** — All projects upgraded simultaneously in a single atomic operation.

Rationale
- Solution size: 1 project (`LogBook.csproj`) — small and self-contained.
- Current target frameworks: `net10.0` for the project; assessment shows no framework changes required.
- NuGet package analysis: 4 packages found; all marked compatible (no suggested upgrades required by the assessment).
- Risk profile: Low — single project, SDK-style, no dependency tree complexity, no security vulnerabilities reported.

Scope
- Repository: `C:\Users\nigel\source\reposNET10\LogBook`
- Solution: `LogBook.sln`
- Projects in scope: `LogBook\LogBook.csproj` (ASP.NET Core web project)

Key outcomes
- Ensure SDK/global.json alignment for target SDK (if present)
- Confirm package compatibility and decide whether to proactively update EF packages from 6.x to later major versions (optional)
- Verify solution builds and tests run against `net10.0`

---

## Migration Strategy

Approach
- Use the All-At-Once Strategy: apply project file updates (if any), package updates (if chosen), restore and build the whole solution, fix compilation issues in one coordinated pass, then run tests and finalize.

Why All-At-Once
- Single small project means atomic update has minimal blast radius.
- No inter-project dependency ordering required beyond the single project itself.
- Simple source control approach is possible (single feature branch + single atomic commit for the upgrade changes).

Phases (descriptive)
- Phase 0: Preparation — SDK validation and global.json review
- Phase 1: Atomic Upgrade — apply all project/package updates in one coordinated operation
- Phase 2: Validation — build, run tests, and address any issues

Note: The plan intentionally avoids prescribing per-file task splitting; the atomic upgrade is a single coordinated pass as required by All-At-Once guidance.

---

## Detailed Dependency Analysis

Summary
- Total projects: 1
- `LogBook.csproj` is both a leaf and a root (no project-to-project dependencies).
- No circular dependencies.

Project dependency map (textual)
- `LogBook.csproj` (net10.0) — no project references

Critical path
- Only `LogBook.csproj`; upgrading it completes the solution migration.

---

## Project-by-Project Plans

### Project: `LogBook.csproj`

Current State
- Path: `LogBook\LogBook.csproj`
- TargetFramework: `net10.0`
- SDK-style: Yes
- Project type: AspNetCore (Microsoft.NET.Sdk.Web)
- Files: 29
- LOC: 934

Dependencies (NuGet)
- `Microsoft.EntityFrameworkCore.Design` 6.0.5
- `Microsoft.EntityFrameworkCore.SqlServer` 6.0.5
- `Microsoft.EntityFrameworkCore.Tools` 6.0.5
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` 6.0.4

Target State
- TargetFramework: `net10.0` (no change)
- NuGet packages: remain compatible; no mandatory package changes per assessment. Optionally, packages may be updated later for feature or security reasons.

Migration Steps (what executor will do in atomic pass)
1. Prerequisites: Verify .NET 10 SDK is installed and `global.json` (if present) is compatible with the SDK required for `net10.0`.
2. If a change to `global.json` is required, update it before the atomic pass.
3. Optionally, decide whether to proactively update EF packages to newer major versions (see Package Update Reference). If chosen, include the package updates in the atomic pass.
4. Ensure any MSBuild imported props/targets (e.g., Directory.Build.props) are considered in the same atomic update if present.
5. Restore dependencies and build the entire solution to detect compilation issues.
6. Fix compilation errors caused by package or framework updates in the same operation.
7. Run test projects (if any) and address failures.
8. Confirm final build is clean (0 errors) and tests pass.

Validation checklist (for this project)
- [ ] `TargetFramework` set to `net10.0` in `LogBook.csproj`
- [ ] All selected package changes applied (if opted-in)
- [ ] `dotnet restore` completes successfully
- [ ] Solution builds with 0 errors
- [ ] Unit/integration tests pass (if present)
- [ ] No outstanding security vulnerabilities reported by tooling

---

## Package Update Reference

Summary
- Assessment reported 4 packages; all marked as compatible with `net10.0`. No suggested versions provided by the assessment.

Package matrix
- `Microsoft.EntityFrameworkCore.Design` — current `6.0.5` — Action: No change required (compatible)
- `Microsoft.EntityFrameworkCore.SqlServer` — current `6.0.5` — Action: No change required (compatible)
- `Microsoft.EntityFrameworkCore.Tools` — current `6.0.5` — Action: No change required (compatible)
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` — current `6.0.4` — Action: No change required (compatible)

Notes and recommendations
- All packages are EF Core 6.x and code currently targets `net10.0`. EF Core 6 is compatible, but consider whether your project should move to a later EF Core major version for new features or long-term support alignment. If you choose to update EF Core major version, include those package updates in the atomic pass and expect API changes to be addressed during compilation fixes.
- No security vulnerabilities were flagged by the assessment. If third-party scanning tools report vulnerabilities outside the assessment, handle them as part of the atomic upgrade.

---

## Breaking Changes Catalog

Assessment results
- No API or behavioral incompatibilities were detected by the automated analysis for `net10.0` migration.

Potential areas to watch (general guidance)
- ASP.NET Core startup/Program changes (only applicable if templates or hosting model differ)
- EF Core API differences if moving EF to a newer major version (not required by assessment)
- Obsolete APIs flagged by analyzers — resolve during build-and-fix pass

?? Requires validation: If package major-version updates are applied (EF Core or others), expect API-level changes to show up during compilation step and plan to address them in the atomic pass.

---

## Testing Strategy

Per-project and full-solution testing
- Discover test projects and run them after the atomic upgrade. Assessment did not list test projects explicitly; if tests exist, include them in Phase 2 validation.

Validation checklist (solution-wide)
- Build completes with 0 errors
- Unit tests run and pass
- Integration tests (if present) run and pass
- Smoke/manual validation documented separately (not part of automated tasks unless tests cover them)

Test execution guidance
- Automated test execution should occur after the atomic upgrade completes and the solution builds.
- Failures must be triaged and fixed as part of the same atomic operation where possible, or flagged for follow-up if they require larger refactors.

---

## Risk Management

Overall risk: Low

Risk factors
- Single project reduces coordination risk
- EF Core 6.x packages are older but compatible — if upgraded they increase risk
- No security vulnerabilities reported by assessment

Mitigations
- Validate SDK and `global.json` before making changes
- Keep backup or create a dedicated upgrade branch and a single atomic commit for the upgrade
- If choosing to upgrade EF Core major version, run focused tests and be prepared for code fixes

Contingency
- If after the atomic pass the solution does not build and cannot be fixed quickly, revert the upgrade branch and investigate errors in isolation. (This is an execution-stage action and not performed here.)

---

## Complexity & Effort Assessment

Per-project complexity
- `LogBook.csproj`: Low complexity — small codebase, no project dependencies, compatible packages.

Phase complexity
- Phase 0 (Preparation): Low — SDK validation and global.json check
- Phase 1 (Atomic Upgrade): Low — likely no changes required; may include package updates if chosen
- Phase 2 (Validation): Low — build and test validation

?? Requires validation: Confirm presence/absence of test projects and any CI build definitions referencing a specific SDK version.

---

## Source Control Strategy

Branching
- Create a short-lived upgrade branch for the atomic upgrade (feature branch off current main branch). All upgrade changes should be made on that branch.

Commit approach (All-At-Once)
- Preferred: A single atomic commit (or a small set of well-scoped commits) that includes all project file changes and package updates required by the upgrade.
- Include a clear commit message referencing the plan and list of changed files.

Pull request and review
- Open a PR from the upgrade branch to `main`/`master` and include a link to this plan and the assessment.
- Run CI on the PR to validate the build and tests before merging.

Note: The plan intentionally recommends a consolidated change set to reflect the All-At-Once strategy and to make review of the atomic upgrade straightforward.

---

## Success Criteria

The migration is considered complete when all the following are true:
1. All projects target their planned frameworks (for this solution: `LogBook.csproj` remains `net10.0`).
2. All package updates included in the atomic upgrade (if any were chosen) are applied.
3. Solution builds with 0 errors.
4. All automated tests (unit/integration) pass.
5. No outstanding security vulnerabilities reported by the scanning tools used by your organization.
6. Upgrade branch is merged following PR review and CI validation (execution-stage activity).

---

## Appendix: Artifacts & References

Files referenced in plan
- `LogBook\LogBook.csproj`
- `LogBook.sln`
- `C:\Users\nigel\source\reposNET10\LogBook\.github\upgrades\assessment.md`

Notes
- This plan is planning-only. It documents what to do and why; it does not execute changes.
- If you want to include optional EF Core major-version upgrades, request that and the plan will be extended with exact package target versions and a breaking-changes catalog for EF Core.


