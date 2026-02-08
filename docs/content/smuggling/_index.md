---
title: Smuggling
layout: wide
toc: false
---

## HTTP Request Smuggling

HTTP request smuggling exploits disagreements between front-end and back-end servers about where one request ends and the next begins. When two servers in a chain parse the same byte stream differently, an attacker can "smuggle" a hidden request past the front-end.

These tests send requests with ambiguous framing &mdash; conflicting `Content-Length` and `Transfer-Encoding` headers, duplicated values, obfuscated encoding names &mdash; and verify the server rejects them outright rather than guessing.

{{< callout type="warning" >}}
Some tests are **unscored** (marked with `*`). These send payloads where the RFC permits multiple valid interpretations &mdash; for example, OWS trimming or case-insensitive TE matching. A `2xx` response is RFC-compliant but shown as a warning since stricter rejection is preferred.
{{< /callout >}}

<div id="lang-filter"></div>
<div id="table-smuggling"><p><em>Loading...</em></p></div>

<script src="/Http11Probe/probe/data.js"></script>
<script src="/Http11Probe/probe/render.js"></script>
<script>
(function () {
  if (!window.PROBE_DATA) {
    document.getElementById('table-smuggling').innerHTML = '<p><em>No probe data available yet. Run the Probe workflow manually on <code>main</code> to generate results.</em></p>';
    return;
  }
  var GROUPS = [
    { key: 'cl-te', label: 'CL + TE Conflicts', testIds: [
      'SMUG-CL-TE-BOTH','SMUG-CLTE-PIPELINE','SMUG-TECL-PIPELINE','SMUG-TE-HTTP10'
    ]},
    { key: 'cl-parsing', label: 'Content-Length Parsing', testIds: [
      'SMUG-DUPLICATE-CL','SMUG-CL-LEADING-ZEROS','SMUG-CL-NEGATIVE',
      'SMUG-CL-COMMA-DIFFERENT','SMUG-CL-OCTAL','SMUG-CL-HEX-PREFIX',
      'SMUG-CL-INTERNAL-SPACE','SMUG-CL-COMMA-SAME',
      'SMUG-CL-TRAILING-SPACE','SMUG-CL-EXTRA-LEADING-SP'
    ]},
    { key: 'te-obfuscation', label: 'Transfer-Encoding Obfuscation', testIds: [
      'SMUG-TE-XCHUNKED','SMUG-TE-TRAILING-SPACE','SMUG-TE-SP-BEFORE-COLON',
      'SMUG-TE-EMPTY-VALUE','SMUG-TE-LEADING-COMMA','SMUG-TE-DUPLICATE-HEADERS',
      'SMUG-TE-NOT-FINAL-CHUNKED','SMUG-TE-IDENTITY',
      'SMUG-TE-DOUBLE-CHUNKED','SMUG-TE-CASE-MISMATCH',
      'SMUG-TRANSFER_ENCODING','SMUG-CHUNKED-WITH-PARAMS'
    ]},
    { key: 'chunk', label: 'Chunk Encoding', testIds: [
      'SMUG-CHUNK-BARE-SEMICOLON','SMUG-CHUNK-HEX-PREFIX','SMUG-CHUNK-UNDERSCORE',
      'SMUG-CHUNK-LEADING-SP','SMUG-CHUNK-MISSING-TRAILING-CRLF',
      'SMUG-CHUNK-EXT-LF','SMUG-CHUNK-SPILL','SMUG-CHUNK-LF-TERM',
      'SMUG-CHUNK-EXT-CTRL','SMUG-CHUNK-LF-TRAILER','SMUG-CHUNK-NEGATIVE'
    ]},
    { key: 'headers', label: 'Headers & Injection', testIds: [
      'SMUG-BARE-CR-HEADER-VALUE','SMUG-HEADER-INJECTION'
    ]},
    { key: 'trailers', label: 'Trailers', testIds: [
      'SMUG-TRAILER-CL','SMUG-TRAILER-TE','SMUG-TRAILER-HOST'
    ]},
    { key: 'method-body', label: 'Method & Body Handling', testIds: [
      'SMUG-EXPECT-100-CL','SMUG-HEAD-CL-BODY','SMUG-OPTIONS-CL-BODY'
    ]}
  ];
  function render(data) {
    var ctx = ProbeRender.buildLookups(data.servers);
    ProbeRender.renderSubTables('table-smuggling', 'Smuggling', ctx, GROUPS);
  }
  render(window.PROBE_DATA);
  ProbeRender.renderLanguageFilter('lang-filter', window.PROBE_DATA, render);
})();
</script>
