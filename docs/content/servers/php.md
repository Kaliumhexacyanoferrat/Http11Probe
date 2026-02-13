---
title: "PHP"
toc: false
breadcrumbs: false
---

**Language:** PHP · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/PhpServer)

## Dockerfile

```dockerfile
FROM php:8.3-cli
COPY src/Servers/PhpServer/index.php /app/index.php
WORKDIR /app
EXPOSE 8080
CMD ["php", "-S", "0.0.0.0:8080", "index.php"]
```

## Source — `index.php`

```php
<?php
if ($_SERVER['REQUEST_URI'] === '/echo') {
    header('Content-Type: text/plain');
    foreach (getallheaders() as $name => $value) {
        echo "$name: $value\n";
    }
    exit;
}

header('Content-Type: text/plain');
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    echo file_get_contents('php://input');
} else {
    echo 'OK';
}
```
