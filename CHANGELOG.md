# Changelog

All notable changes to Http11Probe are documented in this file.

## [2026-02-14]

### Added
- **RFC Level indicator row** — result tables now show a translucent capsule (MUST/SHOULD/MAY/N/A) for each test, indicating the RFC 2119 requirement level
- **Method indicator row** — result tables show the HTTP method (GET, POST, etc.) for each test in an outlined monospace badge style
- **Method filter** — filter result tables by HTTP method (GET, POST, HEAD, etc.) on all category pages
- **RFC Level filter** — filter result tables by RFC requirement level (MUST, SHOULD, MAY, N/A) on all category pages
- **Method & RFC Level in popup** — server detail modal now includes Method and RFC Level columns alongside Test, Expected, Got, and Description
- **`RfcLevel` enum** — `Must`, `Should`, `May`, `OughtTo`, `NotApplicable` classification for every test case
- **RFC Level annotations** — all tests across Compliance, Smuggling, MalformedInput, and Normalization suites annotated with their RFC 2119 requirement level
- **Verbose Probe workflow** — new `probe-verbose.yml` GitHub Action for manual single-server probing with `--verbose` output, triggered via `workflow_dispatch` with a server name input (#60)
- **9 new RFC 9110 compliance tests** sourced from [mohammed90/http-compliance-testing](https://github.com/mohammed90/http-compliance-testing):
  - `COMP-HEAD-NO-BODY` — HEAD response must not contain a message body (RFC 9110 §9.3.2, MUST)
  - `COMP-UNKNOWN-METHOD` — unrecognized method should be rejected with 501/405 (RFC 9110 §9.1, SHOULD)
  - `COMP-405-ALLOW` — 405 response must include Allow header (RFC 9110 §15.5.6, MUST)
  - `COMP-DATE-HEADER` — origin server must include Date header in responses (RFC 9110 §6.6.1, MUST)
  - `COMP-NO-1XX-HTTP10` — server must not send 1xx to HTTP/1.0 client (RFC 9110 §15.2, MUST NOT)
  - `COMP-NO-CL-IN-204` — Content-Length forbidden in 204 responses (RFC 9110 §8.6, MUST NOT)
  - `SMUG-CL-COMMA-TRIPLE` — three comma-separated identical CL values (RFC 9110 §8.6, unscored)
  - `COMP-OPTIONS-ALLOW` — OPTIONS response should include Allow header (RFC 9110 §9.3.7, SHOULD)
  - `COMP-CONTENT-TYPE` — response with content should include Content-Type (RFC 9110 §8.3, SHOULD)

### Changed
- **AGENTS.md** — added Step 5 (RFC Requirement Dashboard) to the "Add a new test" task; added Step 5 (server documentation page) to the "Add a framework" task
- **RFC Requirement Dashboard** — updated with all 9 new tests, counts, and cross-references
- **Landing page cards** — removed hardcoded test count from RFC Requirement Dashboard subtitle
- **Score calculation** — warnings now included in the overall score (#66)

### Fixed
- **Caddy server** — fixed POST body echo using Caddy Parrot pattern; updated Caddyfile, Dockerfile, and docs page
- **Lighttpd server** — fixed POST body echo implementation (#57)
- **HAProxy server** — fixed POST / endpoint (#64)
- **Echo validation** — empty body now correctly returns Fail; body mismatch returns Fail; chunked transfer encoding properly decoded before comparison (#61)
- **Validator ordering** — fixed 8 tests where connection-state check ran before response-status check, preventing false passes when server returned 2xx then closed (COMP-POST-CL-UNDERSEND, RFC9112-2.3-HTTP09-REQUEST, MAL-BINARY-GARBAGE, MAL-INCOMPLETE-REQUEST, MAL-EMPTY-REQUEST, MAL-WHITESPACE-ONLY-LINE, MAL-H2-PREFACE, MAL-POST-CL-HUGE-NO-BODY)
- **COMP-CHUNKED-NO-FINAL validator** — fixed same ordering bug where connection close was accepted even when server returned 2xx
- **Method extraction** — handles leading CRLF in raw requests and tab-delimited request lines; non-HTTP pseudo-methods (PRI) shown as '?'
- **Category-scoped filters** — Method and RFC Level filters now only show options relevant to the current category page

## [2026-02-12]

### Added
- **Request/response detail tooltips** — hover over a result pill to see the raw response; click to open a modal with both the raw request and response (#27)
- Repository cleanup — removed clutter files (probe-glyph.json, pycache, package-lock.json, DotSettings.user)

### Fixed
- BARE-LF tests (RFC 9112 §2.2) adjusted to warn on 2xx instead of fail, matching RFC SHOULD-level requirement (#21)

### Removed
- Proxy compliance tests removed from the suite (#20)

## [2026-02-11]

### Added
- POST endpoint for Kestrel (ASP.NET Minimal) server (#13)
- POST endpoint for Quarkus server (#14)
- POST endpoint for Spring Boot server (#16)
- POST endpoint for Express server (#17)

### Fixed
- H2O server now allows POST commands (#19)
- Flask server routing and default port (#11)
- SimpleW server POST handling and version update (#5)

## [2026-02-09]

### Added
- SimpleW server contributed by stratdev3 (#2)

### Fixed
- Glyph server — reset request state on each new connection (#3)
- In-development frameworks now filtered from results (#4)
- SimpleW removed from blacklisted servers

## [2026-02-08]

### Added
- **30 new tests** — body/content handling, chunked TE attack vectors, and additional compliance/smuggling tests (46 → 80 → 110+)
- **7 new servers** — Actix, Ntex, Bun, H2O, NetCoreServer, Sisk, Watson
- **6 more servers** — GenHTTP, SimpleW, EmbedIO, Puma, PHP, Deno, and others (total: 36)
- **Deep analysis docs** — verified RFC evidence and ABNF grammar added to all glossary pages
- **Exact HTTP request code blocks** in all glossary pages
- **Category filter** — filter probe results by Compliance, Smuggling, or Malformed Input
- **Language filter** — filter servers by programming language
- **Sub-tables** — result tables split into logical groups within each category
- **Unscored tests** — separate bucket for RFC-compliant reference tests, shown with reduced opacity and asterisk
- **CLI improvements** — `--test` filter, `--help`, docs links in output, selected test display
- **Summary bar chart** — ranked bars replacing summary badges, with pass/warn/fail/unscored segments
- **Scrollbar styling** — themed scrollbars for probe result tables
- **Custom favicon** — shield icon for browser tab
- **Docs logo** — minimal shield outline

### Fixed
- Summary fail count derivation so pass + warn + fail = total
- Unscored double-counting in summary statistics
- Sort order: rank by scored pass + scored warn only
- Puma Dockerfile: install build-essential for nio4r native extension
- Deno Dockerfile: use `latest` tag instead of nonexistent `:2`
- FRAGMENT-IN-TARGET re-scored as strict (implicit grammar prohibition)
- Nancy and Nginx failing to start in CI
- All servers bound to `0.0.0.0` for Docker reachability

### Removed
- Redundant SMUG-HEADER-INJECTION test (covered by other smuggling tests)
- Nancy server removed from probe (no probe.json)

## [2026-02-07]

### Added
- **Initial release** — extracted from Glyph11 into standalone Http11Probe repository
- 12 standalone test servers dockerized with Docker Compose
- Sequential probe workflow — one server at a time on port 8080
- CI probe workflow (`.github/workflows/probe.yml`) with STRICT expectations dictionary
- Hugo + Hextra documentation site with glossary, per-test docs, and probe results pages
- Separate pages for Compliance, Smuggling, Malformed Input categories
- Landing page with platform framing and contributor onboarding
- "Add a Framework" documentation page

### Fixed
- Docker image tags lowercased as required
- Git worktree/orphan branch creation for latest-results
- GlyphServer: replaced manual buffer with PipeReader, fixed closing without response on oversized requests
- Pingora build: added cmake and g++ to build stage
