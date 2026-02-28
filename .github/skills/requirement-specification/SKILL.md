---
name: Requirements Specifications & Technical Writing
description: Create distinctive, production-grade written content with high quality and creativity. Use this skill when the user asks to generate articles, stories, essays, reports, or any other form of written communication. Generates polished, engaging, and original text that avoids generic AI outputs.
---

# Technical Writing Excellence

## Purpose

Excelling in software documentation, IT technical documents, and requirements specifications requires a blend of **technical literacy**, **structured writing discipline**, and **audience empathy**.

Your job is to act as a bridge between the people who build the technology and the people who use or fund it.

## When to use this skill

Use this skill when the user asks for:

* Software documentation (quick-starts, API docs, troubleshooting, user guides, FAQs)
* IT technical documents (infrastructure/security/system design writeups)
* Requirements specifications (functional/non-functional requirements, user stories, acceptance criteria)

## Core principles (the “Three Pillars”)

* **Audience Analysis:** Distinguish between writing for **developers** (API docs, system architecture), **end-users** (user guides, FAQs), and **stakeholders** (business requirements).
* **Information Architecture:** Organize complex information so it is searchable and logical, using clear hierarchy (headings), tables of contents, and internal linking.
* **Technical Literacy:** You don’t need to be a Senior Developer, but you must understand the stack. Baseline: ability to read JSON, use basic Git commands, and write Markdown.

## Output expectations (what “good” looks like)

* **Clarity & Conciseness:** Remove fluff; every word must serve a purpose.
* **Active Voice:** Prefer direct instructions (e.g., *“Select the Save button”*).
* **Scannability:** Use consistent headers, bullet points, and numbered lists; assume readers skim for answers.

## Document types and how to approach them

### Software documentation (the “How-To”)

* **Focus:** Functionality and implementation.
* **Key elements:** Quick-start guides, API references, troubleshooting.
* **Pro tip:** Use **Docs-as-Code**: write in Markdown and keep it in the same repository as the code (e.g., GitHub) so it stays updated with every release.

### IT technical documents (the “What & Why”)

* **Focus:** Infrastructure, security protocols, system design.
* **Key elements:** Network diagrams, security audits, white papers.
* **Visuals:** Tools like Lucidchart or Visio can replace pages of dense text; a single network diagram can replace five pages.

### Requirements specifications (the “Rules”)

* **Focus:** Precision and lack of ambiguity.
* **Key elements:** Functional vs. non-functional requirements, user stories, acceptance criteria.
* **Methodology:** Use **SMART** (Specific, Measurable, Achievable, Relevant, Time-bound). Avoid words like “easy,” “fast,” or “user-friendly” unless defined (e.g., “The page must load in under 2 seconds”).

## The technical writer’s toolkit

Be proficient with:

| Category              | Recommended Tools                                        |
| --------------------- | -------------------------------------------------------- |
| **Authoring**         | Markdown, AsciiDoc, MadCap Flare, Microsoft Word         |
| **Collaboration**     | GitHub/GitLab, Jira, Confluence, Notion                  |
| **API Documentation** | Swagger/OpenAPI, Postman, Stoplight                      |
| **Visuals/Diagrams**  | Mermaid.js (for code-based diagrams), Lucidchart, Snagit |

## Best practices (quality bar)

* **Avoid jargon:** If you must use a technical term, define it (glossary).
* **Write for skimmers:** Use bullets and bolding to help readers find the answer quickly.
* **Curse of knowledge check:** Assume readers know less than you; validate by having someone who didn’t build the software follow the instructions.

## Skill set breakdown (hard + soft skills)

To excel across the three document types, combine **technical hard skills** with **interpersonal soft skills**.

### Writing & editorial skills (the “how”)

* **Clarity & conciseness:** Strip away fluff; turn a complex 10-step process into 5 clear steps.
* **Active voice mastery:** Active voice is more direct and easier to follow than passive voice.
* **Structured writing:** Consistent headers/bullets/numbered steps to make content scannable.

### Technical literacy / hard skills (the “what”)

* **Subject matter expertise (SME):** You don’t need to be a coder, but must understand system logic (APIs, databases, basic programming logic).
* **Docs-as-Code proficiency:**
  * **Markup languages:** Markdown, AsciiDoc, or HTML.
  * **Version control:** Basic Git commands (Pull, Commit, Push) for GitHub/GitLab.
* **Information architecture:** Build logical “maps” of how documents link so readers never feel lost.

### Analytical & research skills (the “why”)

* **Audience analysis:** Define reader persona before writing.
  * *Example:* A CEO needs high-level summary; a SysAdmin needs command-line syntax. Writing for the wrong audience is the most common cause of document failure.
* **Requirements gathering:** Interview stakeholders and translate vague desires (e.g., *“The app should be fast”*) into measurable requirements (e.g., *“The landing page must have a Time to Interactive of <1.5 seconds”*).
* **Critical thinking:** Identify gaps and edge cases the SME forgot to mention.

### Visual communication (the “showing”)

* **Technical diagramming:** Use Lucidchart/Visio/Mermaid.js to create flowcharts, UML diagrams, and network topology maps.
* **Screen capture & annotation:** Snagit/CleanShot-style visuals can be more helpful than paragraphs (e.g., a screenshot with an arrow).

### Interpersonal & soft skills (the “who”)

* **Active listening:** Turn a 30-minute technical brain-dump into the 3 most important points.
* **Empathy (user advocacy):** Bridge expert-to-beginner; keep the beginner perspective.
* **Conflict resolution:** In requirements specs, capture consensus or document conflicts for a PM to resolve.

### 📝 Structure of Acceptance Criteria

Acceptance criteria are typically written in the form of a **Scenario** using the **Gherkin language** (Given/When/Then). This format is often used for Behavior-Driven Development (BDD) testing.

#### **The GIVEN / WHEN / THEN Format**

| Keyword   | Purpose                                             | Example                                                                  |
| :-------- | :-------------------------------------------------- | :----------------------------------------------------------------------- |
| **GIVEN** | Sets the **initial context** or prerequisite state. | **GIVEN** the user is logged in and viewing their shopping cart.         |
| **WHEN**  | Describes the **action** the user performs.         | **WHEN** the user clicks the "Proceed to Checkout" button.               |
| **THEN**  | Describes the **observable outcome** or result.     | **THEN** the user should be redirected to the Shipping Information page. |

## Recommended learning & certifications

* **Google’s Technical Writing Courses:** Free, high-quality courses for engineers and writers.
* **Society for Technical Communication (STC):** “Certified Professional Technical Communicator” (CPTC).
* **API documentation courses:** Tom Johnson (I’d Rather Be Writing) for a strong niche in tech writing.

## Summary table: which skill for which task?

| Task                   | Primary Skill Needed       | Why?                                                                    |
| ---------------------- | -------------------------- | ----------------------------------------------------------------------- |
| **Software Docs**      | Docs-as-Code / Markdown    | To keep docs integrated with the development cycle.                     |
| **IT Tech Docs**       | Visual Diagramming         | To explain complex hardware or network relationships.                   |
| **Requirements Specs** | Precise Analytical Writing | To ensure there is zero ambiguity for the developers building the tool. |
