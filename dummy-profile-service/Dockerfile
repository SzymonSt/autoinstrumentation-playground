FROM python:3.9

WORKDIR /app

COPY . /app/

RUN python -m venv venv && \
    /bin/bash -c "source venv/bin/activate" && \
    pip install --no-cache-dir -r requirements.txt

CMD ["python", "-u", "main.py"]
