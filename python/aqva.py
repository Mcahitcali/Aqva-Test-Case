import configparser
import requests
from bs4 import BeautifulSoup
from elasticsearch import Elasticsearch

config = configparser.ConfigParser()
config.read("config.ini")

base_domain = config.get("DEFAULT","BASE_DOMAIN")
es_url = config.get("DEFAULT","ES_ENDPOINT")
es_api_key = config.get("DEFAULT","ES_API_KEY")

client = Elasticsearch(es_url, api_key=es_api_key)

# crawling logic
response = requests.get(base_domain)
soup = BeautifulSoup(response.content, "html.parser")

link_elements = soup.select("a[href]")
for link_element in link_elements:
    url = link_element.attrs['href']
    title = link_element.get_text()

    if base_domain in url:
        client.index(
            index='sozcu-urls',
            document={
                'url': url,
                'title': title
            })