##Representative snippet focusing on the GenAI prompt and processing

def get_token_count(text, tokenizer):
    token_len=len(tokenizer.encode(text))
    #app.print_to_text_box(str(token_len))
    return token_len


def tag_part(context, endpoint, key):
    """get sentiment, tag, summary by AI"""
    client = AzureOpenAI(
        azure_endpoint=endpoint,
        api_key=key,
        api_version=env.GPT_API_VERSION
    )
    #app.print_to_text_box(context)
    key_prompt = progress_tracker.get_prompt()
    translate_prompt = '''
        Role: You are a precise Sentiment Analysis & Classification Engine.
        Task: Analyse the provided user review and map it to the predefined tags. For each identified tag, extract the positive and negative sentiments.
        
    ''' + key_prompt + '''

        1. Classification: A review may correspond to zero, one, or multiple tags.
        2. Null Case: If no tags match the content, return only the string: 'No tag'.
        3. Summarisation: Use concise, professional British English. Your summary for each tag must be more succinct than the original review text.
        4. Output Format: Return a valid JSON object only. Do not include introductory remarks, explanations, or markdown prose.

        JSON Structure:
        {"positive":{"TagName": "Brief summary of pros", ...},"negative":{"TagName": "Brief summary of cons", ...}}

        '''

    messages = [{"role": "system", "content": translate_prompt},
                {"role": "user", "content": context}]
    reason = ''
