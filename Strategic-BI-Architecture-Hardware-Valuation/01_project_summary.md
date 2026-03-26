<h1>Strategic Analytics: Unifying Hardware Sell-in & Cloud Activation Data</h1>


<h2>🏢 The Business Challenge: The "Data Blind Spot"</h2>
In the smart living sector, hardware "sell-in" (shipping to retailers/ distirbutors) does not equate to "sell-through" (end user usage). Our organisation faced a critical "data blind spot":<br>
1. Siloed Intelligence: Sales data (ERP) and activation data (Cloud) existed in separate ecosystems with non-matching identifiers.<br>
2. Valuation Gap: Activation data lacked financial metrics, making it impossible to calculate the true ROI of marketing spend or regional sell-through sales performance.<br>



<h2>🛠️ The Solution: End-to-End BI Architecture</h2>
I architected an end-to-end analytics solution to unify financial and technical data into a single Power BI dashboard, which the company is switching from other visualisation tool to Power BI.

**Schema Bridging:** Designed an intermediate mapping layer to resolve 1:N (one-to-many) relationship conflicts between hardware product keys (sales data) and app product activation keys (cloud).

**Dynamic Valuation Engine:** Developed a 3-month rolling unit price algorithm to "assign" financial value to technical activation events. The model featured a "data-drift" logic that automatically pulled proxy values from previous months or adjacent products / markets if real-time sales data was unavailable.

**Enterprise-Grade Governance:** Built a unified semantic model within Power BI, implementing Row-Level Security (RLS) to manage data privacy for 200+ global stakeholders, ensuring regional teams saw only their relevant data while providing the Executive Team with a "Global View."

**Architect Execution:** Managed the data connection from 2 systems and data transformation to DAX-heavy modelling and dashboard design.



<h2>📈The Impact & Strategic Value</h2>

**Market Performance Equity:** Developed a Volume-to-Value metric that neutralised volume bias by correlating hardware activations with total sales value. This shifted executive strategies not just focusing on high runner products but also high-yield "premium performers" (high margin, low volume) optimising resource allocation.<br>

**Unified commercial visibility:** Engineered the organization’s first real-time sell-through metric, consolidating disparate sell-in and activation data into a single source of truth for the executive leadership team and marketing teams.<br>

**Data democratization:** Scaled analytics capabilities by replacing manual ad-hoc reporting with a self-service dashboard. This reduced team-wide manual overhead and eliminated reporting discrepancies across time shifts.



<h2>🔮 Future Roadmap: Supply Chain Intelligence</h2>

The next phase involves integrating manufacturing data to bridge the gap between production and activation:

**Inventory Optimisation:** Calculating lead times from factory to end-user to identify overstock risks and trigger local marketing actions for slow-moving SKUs.

**Geographic Logistics Refinement:** Analysing regional lags to suggest localised supply chain adjustments and reduce shipping latency.



<h2>⚙️ Technical Stack</h2>

Tool: Power BI (Desktop & Service)

Modelling: DAX, Semantic Layer Design

Security: Static & Dynamic Row-Level Security (RLS)

Data Sources: Snowflake (Sales) & Cloud DB (Activations)
