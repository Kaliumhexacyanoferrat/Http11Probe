---
title: Probe Results
layout: wide
toc: false
---

HTTP/1.1 compliance comparison across frameworks. Each test sends a specific malformed or ambiguous request and checks the server's response against the **exact** expected status code. Updated on each manual probe run on `main`.

## Summary

<div id="lang-filter" style="margin-bottom:6px;"></div>
<div id="cat-filter" style="margin-bottom:16px;"></div>
<div id="probe-summary"><p><em>Loading probe data...</em></p></div>

{{< callout type="info" >}}
These results are from CI runs (`ubuntu-latest`). Click on the **Compliance**, **Smuggling**, or **Malformed Input** tabs above for detailed results per category.
{{< /callout >}}

<script src="/Http11Probe/probe/data.js"></script>
<script src="/Http11Probe/probe/render.js"></script>
<script>
(function () {
  if (!window.PROBE_DATA) {
    document.getElementById('probe-summary').innerHTML = '<p><em>No probe data available yet. Run the Probe workflow manually on <code>main</code> to generate results.</em></p>';
    return;
  }
  var langFiltered = window.PROBE_DATA;
  var catFilter = null;

  function rerender() {
    var data = langFiltered;
    if (catFilter) {
      data = ProbeRender.filterByCategory(data, catFilter);
    }
    ProbeRender.renderSummary('probe-summary', data);
  }

  ProbeRender.renderSummary('probe-summary', window.PROBE_DATA);
  ProbeRender.renderLanguageFilter('lang-filter', window.PROBE_DATA, function (filtered) {
    langFiltered = filtered;
    rerender();
  });
  ProbeRender.renderCategoryFilter('cat-filter', function (categories) {
    catFilter = categories;
    rerender();
  });
})();
</script>
