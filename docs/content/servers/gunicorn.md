---
title: "Gunicorn"
toc: false
breadcrumbs: false
---

**Language:** Python · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/GunicornServer)

## Dockerfile

```dockerfile
FROM python:3.13-slim
RUN pip install --no-cache-dir gunicorn
COPY src/Servers/GunicornServer/app.py /app/app.py
WORKDIR /app
EXPOSE 8080
CMD ["gunicorn", "-b", "0.0.0.0:8080", "app:app"]
```

## Source — `app.py`

```python
def app(environ, start_response):
    start_response('200 OK', [('Content-Type', 'text/plain')])
    if environ['REQUEST_METHOD'] == 'POST':
        try:
            length = int(environ.get('CONTENT_LENGTH', 0) or 0)
        except ValueError:
            length = 0
        body = environ['wsgi.input'].read(length) if length > 0 else b''
        return [body]
    return [b'OK']
```
