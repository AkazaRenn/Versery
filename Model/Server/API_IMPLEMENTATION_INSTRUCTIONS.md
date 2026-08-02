# API Implementation Instructions

## Purpose
This guide defines how AI agents must sync Mastodon documentation to code under Model/Server.
Use this for future updates to methods and entities.

## Source of Truth
- Documentation root: Model/Server/Documentations/content/en
- Methods docs: Model/Server/Documentations/content/en/methods
- Entities docs: Model/Server/Documentations/content/en/entities

## Global Rules
1. Check the documentation first. Do not trust existing code versions blindly.
2. Do not reformat unrelated code.
3. Do not change code only for style.
4. Ignore consumers unless explicitly requested.
5. Keep changes minimal and scoped.
6. Validate updated file(s) with compile diagnostics.

### JsonContext registration
1. When adding a new type under any subfolder of Model/Server/Entities, add it to Model/Server/JsonContext.cs.
2. For types under Entities subfolders, set TypeInfoPropertyName using folder-prefixed naming to avoid collisions.
	- Format: Folder1_Folder2_ClassName
	- Example: Entities/Admin/Account => Admin_Account
3. If a new top-level entity type (directly under Model/Server/Entities) is not referenced by another type already registered in JsonContext, add it explicitly to JsonContext.

---

## Methods Sync Rules (Model/Server/Methods)

### A. Documentation and naming
1. Each API method must include XML documentation with:
	- Latest version from docs.
	- Link to Mastodon docs.
2. Method name must use the endpoint hashtag section from docs.
	- Example: methods/accounts/#create => Create

### B. Signature and arguments
1. Add any missing APIs from docs.
2. Arguments must match docs exactly in meaning and order.
3. Use camelCase for argument names.
4. Input enums are not supported for now; use string for inputs.

### C. Collection contracts
1. Output list types must be List<T>.
2. Input collection types must be IEnumerable<T>.

### D. Response entity handling
1. If response maps to an entities/* doc, the type must exist under Model/Server/Entities.
2. If missing, create it.
3. If type is not from entities folder docs, define it in the same interface file.
4. For all response entities under Model/Server/Entities, follow the Entities sync rules in this document.

---

## Entities Sync Rules (Model/Server/Entities)

### A. Documentation and class-level link
1. Add top-level entity doc link.
2. Each property must have XML docs including:
	- Version: x.x.x
	- Link to exact docs anchor.
3. Use latest version from docs.

### B. Property design
1. Property names must be PascalCase based on doc field names.
2. Fix incorrect property names where needed.
3. Property order must match docs order.
4. Every property must have an explicit default value.

### C. Type mapping
1. URL fields => Uri?
2. Locale strings => CultureInfo (or CultureInfo? if nullable)
3. Lists/arrays => List<T>
4. oneOf strings => enum with JsonStringEnumMemberName

### D. Enum ownership
1. Reuse an existing enum when semantically equivalent and ownership/location rules already match.
2. If no reusable enum exists, put oneOf enums in the same file as the owning entity.
3. If an owner enum exists in Entities/Enumerations, move it into owner file.

### E. Nested types and inheritance
1. Name nested types clearly and specifically.
	- Example: AdminMeasureData for AdminMeasure.Data
2. Use inheritance where appropriate and documented.
3. Do not add class-level comments for inner classes/enums when property-level docs already exist.
	- Only add class-level comment when there are no per-property docs.

### F. Deprecation handling
1. If docs mark a property as deprecated, add Obsolete with reason copied from docs text.
2. Do not paraphrase deprecation reason.

---

## Execution checklist

### For methods
1. Read matching methods/*.md doc.
2. Compare interface signatures, names, versions, and links.
3. Add missing APIs and fix argument order/names/types.
4. Ensure list input/output rules are followed.
5. Ensure response entities exist and follow the Entities sync rules.
6. Run diagnostics and fix issues related to this change.

### For entities
1. Read matching entities/*.md doc.
2. Compare property order, names, types, defaults, and version anchors.
3. Add/fix XML docs and top-level entity link.
4. Apply type mapping and enum ownership rules.
5. Add Obsolete when deprecated in docs, using copied reason text.
6. Run diagnostics and fix issues related to this change.

---

## Definition of Done
- Docs and code are aligned for the target interface/entity.
- XML docs include correct latest version and link anchors.
- Types and defaults follow this guide.
- Any required enums are colocated with owner type.
- File-level diagnostics pass for changed files.
