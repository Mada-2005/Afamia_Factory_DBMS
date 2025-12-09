# Quick Reference - UI Components

## 🎨 Most Used Button Styles

### Primary Button (Purple Gradient)
```html
<button class="btn btn-modern btn-modern-primary">
    Button Text
</button>
```

### Success Button (Green Gradient)
```html
<button class="btn btn-modern btn-modern-success">
    Button Text
</button>
```

### Danger Button (Red Gradient)
```html
<button class="btn btn-modern btn-modern-danger">
    Delete
</button>
```

### Info Button (Blue Gradient)
```html
<button class="btn btn-modern-info btn-sm">
    Edit
</button>
```

### Outline Button
```html
<button class="btn btn-modern btn-modern-outline">
    Cancel
</button>
```

---

## 🎯 Page Title Pattern

```html
<div class="page-header">
    <h1 class="page-title">Your Page Title</h1>
    <p style="color: #7f8c8d; font-size: 15px;">Your subtitle or description</p>
</div>
```

Add this CSS to your page's `<style>` block:
```css
.page-title {
    font-size: 32px;
    font-weight: 700;
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
}
```

---

## 📊 Modern Table Pattern

```html
<div class="table-container">
    <table class="table-modern">
        <thead>
            <tr>
                <th>Column 1</th>
                <th>Column 2</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Data 1</td>
                <td>Data 2</td>
                <td>
                    <a class="btn btn-modern-info btn-sm" href="#">Edit</a>
                </td>
            </tr>
        </tbody>
    </table>
</div>
```

Required CSS (add to page's `<style>` block):
```css
.table-container {
    background: white;
    border-radius: 20px;
    padding: 0;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
    overflow: hidden;
}

.table-modern thead {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.table-modern thead th {
    color: white;
    font-weight: 600;
    padding: 20px 18px;
    text-transform: uppercase;
    font-size: 13px;
}

.table-modern tbody tr:hover {
    background: linear-gradient(90deg, rgba(102, 126, 234, 0.08) 0%, rgba(118, 75, 162, 0.08) 100%);
    transform: scale(1.01);
}
```

---

## 📝 Modern Form Input

```html
<div class="form-group-enhanced">
    <label class="label-enhanced">Field Label</label>
    <input type="text" class="input-enhanced" placeholder="Enter value">
</div>
```

Required CSS:
```css
.label-enhanced {
    font-weight: 600;
    color: #555;
    margin-bottom: 10px;
    font-size: 14px;
}

.input-enhanced {
    border: 2px solid #e8e8e8;
    border-radius: 14px;
    padding: 14px 18px;
    font-size: 15px;
    transition: all 0.3s ease;
    background: #fafafa;
    width: 100%;
}

.input-enhanced:focus {
    border-color: #667eea;
    background: white;
    box-shadow: 0 0 0 4px rgba(102, 126, 234, 0.1);
    outline: none;
    transform: translateY(-2px);
}
```

---

## 🔍 Modern Search Bar

```html
<form method="post" class="search-modern">
    <input type="text" name="SearchTerm" placeholder="Search..." />
    <button type="submit">Search</button>
</form>
```

The `.search-modern` class is already defined in `components.css`!

---

## 🏷️ Status Badges

```html
<!-- Success Badge (Green) -->
<span class="badge-factory badge-yes">Active</span>

<!-- Inactive Badge (Gray) -->
<span class="badge-factory badge-no">Inactive</span>
```

CSS:
```css
.badge-factory {
    display: inline-block;
    padding: 6px 14px;
    border-radius: 20px;
    font-size: 12px;
    font-weight: 600;
}

.badge-yes {
    background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
    color: white;
}

.badge-no {
    background: linear-gradient(135deg, #bdc3c7 0%, #95a5a6 100%);
    color: white;
}
```

---

## 📱 Action Bar (Top of Page)

```html
<div class="action-bar">
    <a class="btn btn-modern btn-modern-primary" href="#">
        <svg width="18" height="18" viewBox="0 0 24 24" fill="none">
            <path d="M12 5v14M5 12h14" stroke="currentColor" stroke-width="2"/>
        </svg>
        Add New
    </a>

    <form method="post" class="search-modern">
        <input type="text" name="Search" placeholder="Search..." />
        <button type="submit">Search</button>
    </form>
</div>
```

CSS:
```css
.action-bar {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 30px;
    gap: 20px;
    flex-wrap: wrap;
}
```

---

## 🎬 Animation Classes

Just add these classes to any element:

```html
<!-- Fade in from bottom -->
<div class="animate-fade-in-up">Content</div>

<!-- Fade in from top -->
<div class="animate-fade-in-down">Content</div>

<!-- Slide in from left -->
<div class="animate-slide-in-left">Content</div>

<!-- Sequential animations (add delays) -->
<div class="animate-fade-in-up stagger-1">First item</div>
<div class="animate-fade-in-up stagger-2">Second item</div>
<div class="animate-fade-in-up stagger-3">Third item</div>
```

---

## 🎨 Common Color Gradients

### Purple (Primary)
```css
background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
```

### Green (Success)
```css
background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
```

### Red (Danger)
```css
background: linear-gradient(135deg, #eb3349 0%, #f45c43 100%);
```

### Blue/Cyan (Info)
```css
background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
```

### Pink
```css
background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
```

---

## 🔗 Icon Resources

All icons in the project use inline SVG. Common icons:

### Plus Icon (Add)
```html
<svg width="18" height="18" viewBox="0 0 24 24" fill="none">
    <path d="M12 5v14M5 12h14" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
</svg>
```

### Edit Icon
```html
<svg width="14" height="14" viewBox="0 0 24 24" fill="none">
    <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7" stroke="currentColor" stroke-width="2"/>
    <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z" stroke="currentColor" stroke-width="2"/>
</svg>
```

### Delete Icon
```html
<svg width="14" height="14" viewBox="0 0 24 24" fill="none">
    <path d="M3 6h18M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" stroke="currentColor" stroke-width="2"/>
</svg>
```

### Search Icon
```html
<svg width="16" height="16" viewBox="0 0 24 24" fill="none">
    <circle cx="11" cy="11" r="8" stroke="currentColor" stroke-width="2"/>
    <path d="M21 21l-4.35-4.35" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
</svg>
```

### Arrow Right Icon
```html
<svg width="16" height="16" viewBox="0 0 24 24" fill="none">
    <path d="M5 12h14M12 5l7 7-7 7" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
</svg>
```

---

## 📋 Complete Page Template

```html
@page
@model YourModel
@{
    ViewData["Title"] = "Page Title";
}

<style>
    .page-header {
        margin-bottom: 35px;
        animation: fadeInDown 0.6s ease-out;
    }

    .page-title {
        font-size: 32px;
        font-weight: 700;
        background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        background-clip: text;
    }

    .action-bar {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 30px;
        gap: 20px;
        flex-wrap: wrap;
    }

    .table-container {
        background: white;
        border-radius: 20px;
        padding: 0;
        box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
        overflow: hidden;
        animation: fadeInUp 0.8s ease-out;
    }

    @@keyframes fadeInDown {
        from { opacity: 0; transform: translateY(-20px); }
        to { opacity: 1; transform: translateY(0); }
    }

    @@keyframes fadeInUp {
        from { opacity: 0; transform: translateY(30px); }
        to { opacity: 1; transform: translateY(0); }
    }
</style>

<div class="container py-4">
    <div class="page-header">
        <h1 class="page-title">Your Page Title</h1>
        <p style="color: #7f8c8d; font-size: 15px;">Page description</p>
    </div>

    <div class="action-bar">
        <a class="btn btn-modern btn-modern-primary" href="#">
            Add New
        </a>

        <form method="post" class="search-modern">
            <input type="text" name="Search" placeholder="Search..." />
            <button type="submit">Search</button>
        </form>
    </div>

    <div class="table-container">
        <!-- Your content here -->
    </div>
</div>
```

---

## ⚡ Pro Tips

1. **Always include fadeIn animations** on page elements
2. **Use gradient text** for important titles
3. **Add hover effects** to all clickable elements
4. **Use icons** alongside text for better UX
5. **Maintain consistent spacing** (20-30px gaps)
6. **Use box-shadow** for depth (0 10px 30px rgba(0,0,0,0.08))
7. **Round corners** generously (12-24px border-radius)

---

**That's it! Copy, paste, and customize! 🚀**
