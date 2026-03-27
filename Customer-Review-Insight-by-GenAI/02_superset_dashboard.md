<h1>Review Dashboard Snippet</h1>


<h2>📂 Structure</h2>

0. Read Me: Data scope, terminology and AI disclaimer
1. Latest review rating overview: KPIs monitoring
2. Latest tag summary
3. Tag distirbution with time series details
4. Review table: searchable with tag, insight category, review summary and original content


<h2>1. Overview section on ratings along with reviews only</h2> 
A quick glance on review trends and performance versus pervious month by various dimensions, including review type (app or e-commerce) and product category.


Comparison of current KPIs against the previous month to identify growth or regressions across key dimensions.
Segment by review tpye and category, to identify platform-specific or category-specific pain points.

![Overviw section on ratings along with reviews only](https://github.com/pian-c/DataAnalyticsProjectDemo/blob/b1e690f102367d7ec47462b1901096f11576e7a9/CustomerReviewInsightByGenAI/05_images/dashboard_overview_review_linked_ratings.png)



<h2>2. Overview section on review tags</h2>
This section translates unstructured feedback into structured business intelligence. By employing automated semantic tagging and sentiment scoring, the analysis moves beyond surface-level ratings to identify the specific drivers of customer satisfaction and friction.

A consolidated Tag Summary that aggregates thousands of raw reviews into high-level thematic blocks, providing a snapshot of the prevailing customer narrative.
A prioritised list of the Top 10 review insight categories, designed to help stakeholders quickly grasp the "voice of customer" and focus on the most impactful areas for improvement.

![Overview_section_tag](https://github.com/pian-c/DataAnalyticsProjectDemo/blob/089ed0468c5e0b60d0b64d3c4701c7d6e787c963/CustomerReviewInsightByGenAI/05_images/dashboard_overview_tag_review_summary.png)


<h2>3. Tag distirbution with time series details</h2>
By monitoring the intersection of volume, sentiment, and tagging, the business can shift from reactive fixes to proactive optimisation.

By correlating review volume with specific tags, can identified "high-impact yopics." This ensures resources are allocated to the issues affecting the largest segment of the user base.
Through MoM distribution benchmarking, the dashboard can flags sudden spikes in specific tags. This serves as an early-warning system for immediate measures.
By monitoring distribution ratio movements, can evaluated the efficacy of implemented solutions. A downward trend in a specific tag provides evidence that a fix successfully improved the user experience.

![details_tag_distribution](https://github.com/pian-c/DataAnalyticsProjectDemo/blob/eb771c69d396acbbfab6a75e21bf74a41468905f/CustomerReviewInsightByGenAI/05_images/dashboard_tag_distribution.png)


