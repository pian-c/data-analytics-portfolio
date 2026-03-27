from dotenv import load_dotenv

load_dotenv()
import os

OPENAI_LIST = os.getenv("OPENAI_LIST")
THREAD_COUNT = os.getenv("THREAD_COUNT")

GPT_MODEL_NAME = os.getenv("GPT_MODEL_NAME")
GPT_API_VERSION = os.getenv("GPT_API_VERSION")

# environment parameters for to access S3
AWS_ACCESS_ID = os.getenv("AWS_ACCESS_ID", None)
AWS_ACCESS_KEY = os.getenv("AWS_ACCESS_KEY", None)
AWS_ASSUME_ROLE_ARN = os.getenv("AWS_ASSUME_ROLE_ARN", None)
AWS_BUCKET_NAME = os.getenv("AWS_BUCKET_NAME", None)

ASSUME_ROLE_FLAG = (os.getenv("ASSUME_ROLE_FLAG", 'False').lower() == 'true')
AWS_REGION = os.getenv("AWS_REGION", 'eu-central-1')
