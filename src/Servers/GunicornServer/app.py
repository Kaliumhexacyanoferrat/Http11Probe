def app(environ, start_response):
    path = environ.get('PATH_INFO', '/')

    if path == '/echo':
        lines = []
        for key, value in environ.items():
            if key.startswith('HTTP_'):
                header_name = key[5:].replace('_', '-').title()
                lines.append(f"{header_name}: {value}")
        if environ.get('CONTENT_TYPE'):
            lines.append(f"Content-Type: {environ['CONTENT_TYPE']}")
        if environ.get('CONTENT_LENGTH'):
            lines.append(f"Content-Length: {environ['CONTENT_LENGTH']}")
        body = ('\n'.join(lines) + '\n').encode('utf-8')
        start_response('200 OK', [('Content-Type', 'text/plain')])
        return [body]

    start_response('200 OK', [('Content-Type', 'text/plain')])
    if environ['REQUEST_METHOD'] == 'POST':
        try:
            length = int(environ.get('CONTENT_LENGTH', 0) or 0)
        except ValueError:
            length = 0
        body = environ['wsgi.input'].read(length) if length > 0 else b''
        return [body]
    return [b'OK']
