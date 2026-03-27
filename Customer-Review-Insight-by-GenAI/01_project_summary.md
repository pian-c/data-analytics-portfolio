<h1>Project: GenAI-Powered Global Customer Review Insights</h1>


<h2>🎯 The Business Challenge: Scaling the voice of the customer (VoC)</h2>
As a global leader in the smart living industry, the company faced an information bottleneck. Thousands of customer reviews were being generated across app stores and e-commerce websites globally.<br>

**Scalability Issue:** Product Managers (PMs) could not spare time to manually read every review across hundreds of SKUs and platforms.

**Technical Debt:** The existing solution relied on formulated Excel keyword mapping, this rule-based logic was rigid, failed to capture sentiment accurately, and required constant manual updates to the keyword library.


<h2>🛠️ The Solution: End-to-End Automated Pipeline</h2>

I architected a hybrid automated system that replaced manual keyword matching with LLM-driven natural language processing.<br>

**Data Orchestration:** Developed Python scripts to automate data retrieval via 3rd-party portal APIs.

**Intelligent Transformation:** Managed the data cleaning and product mapping logic to ensure global reviews were correctly associated with internal product categories and codes.

**GenAI Integration:** Collaborated with data scientists to implement a GPT-based model for:<br>
    _Multi-Label Tagging:_ Categorising reviews into pre-defined functional topics (e.g., Connectivity, UI/UX, Featured request) to prevent over-classification.<br>
    _Sentiment Analysis:_ Identifying nuanced customer emotions beyond simple star ratings.<br>
    _Tag Summarisation:_ Consolidating hundreds of reviews into concise, monthly executive summaries per topic.<br>
    _Review Summarisation:_ Summarising each reviews into 1 sentence per tagging, shorten the manual reading time when necessary.<br>

**Quality Governance:** Conducted rigorous user acceptance testing (UAT) by comparing AI outputs against a manual "golden dataset," achieving over 80% accuracy before full deployment.


<h2>📈 Business Impact & Value Delivered</h2>

**Efficiency Gain:** Reduced manual review and reporting latency for each product managers by 3+ hoursper month, allowing for faster sprint cycles.

**From Qualitative to Quantitative:** Transformed thousands of unstructured global reviews into a structured dataset. By associating tags, sentiment, review summary with product categories and review volume, allowed product managers to measure voice of customer and hence define importance and priority.

**Data-Driven Roadmap:** Enabled PMs to quantify specific product pain points, leading to hardware, software feature, UI/UX improvements and higher Customer Satisfaction (CSAT) scores.

**Global Visibility:** Built an interactive dashboard, providing regional teams with instantaneous, self-service access to customer sentiment.

<h2>💡 Sample Insights</h2>

**Product Roadmap Optimisation:** By isolating the "Feature Request" tag, I identified the most frequent user demands. These data-backed insights allowed the product management team to objectively re-prioritise the development roadmap, ensuring that new features directly align with customer satisfaction goals.

**Friction Point Identification:** Data showed that "Set up" was the highest-volume friction tag. A granular deep-dive revealed a significant drop-off point during Step X input. This insight triggered a cross-functional review between UI/UX and backend engineering teams to streamline the process and implement robust error-handling measurements.


<h2>⚙️ Tech Stack</h2>

Tool: Pycharm

Data Transformation: Excel/Google Sheets, Python

Data Modelling: GenAI/LLM (tagging & summarisation)

Data Visualisation: Apache Superset

Data Sources: API with Python


