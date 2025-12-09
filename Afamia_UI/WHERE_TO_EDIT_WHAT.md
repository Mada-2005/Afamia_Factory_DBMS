# Where To Edit What - Quick Guide

## 🎯 "I Want To Change..."

### 1. Change Colors

#### Change Main Purple Gradient
**File**: Any `.cshtml` page
**Find**: `linear-gradient(135deg, #667eea 0%, #764ba2 100%)`
**Edit**: Change the hex codes
```css
/* BEFORE */
background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);

/* AFTER - Example with blue */
background: linear-gradient(135deg, #3b82f6 0%, #1d4ed8 100%);
```

**Color Picker Tools:**
- https://colorhunt.co (find color palettes)
- https://cssgradient.io (create gradients)

#### Change Text Color
**File**: Any `.cshtml` page
**Find**: `color: #somecolor;`
```css
color: #667eea;    /* Purple */
color: #2c3e50;    /* Dark gray */
color: #7f8c8d;    /* Light gray */
```

---

### 2. Change Animation Speed

#### Make Animations Faster/Slower
**File**: Any `.cshtml` page or `animations.css`
**Find**: Animation duration (seconds or ms)
```css
/* BEFORE - animates for 0.6 seconds */
animation: fadeIn 0.6s ease-out;

/* AFTER - faster animation (0.3 seconds) */
animation: fadeIn 0.3s ease-out;

/* AFTER - slower animation (1.2 seconds) */
animation: fadeIn 1.2s ease-out;
```

#### Change Transition Speed
**File**: Any `.cshtml` page
**Find**: `transition: all 0.3s ease;`
```css
/* BEFORE */
transition: all 0.3s ease;

/* AFTER - slower transitions */
transition: all 0.6s ease;
```

---

### 3. Change Spacing

#### Increase/Decrease Padding (Space Inside Elements)
**File**: Any `.cshtml` page
**Find**: `padding:` property
```css
/* BEFORE */
padding: 30px;

/* AFTER - more space inside */
padding: 50px;

/* Different padding on each side */
padding: 20px 30px;        /* 20px top/bottom, 30px left/right */
padding: 10px 20px 30px 40px;  /* top right bottom left */
```

#### Increase/Decrease Margin (Space Outside Elements)
**File**: Any `.cshtml` page
**Find**: `margin:` or `margin-bottom:` property
```css
/* BEFORE */
margin-bottom: 40px;

/* AFTER - more space below */
margin-bottom: 60px;
```

#### Increase/Decrease Gap (Space Between Elements)
**File**: Any `.cshtml` page
**Find**: `gap:` property
```css
/* BEFORE */
gap: 20px;

/* AFTER - elements closer together */
gap: 10px;
```

---

### 4. Change Rounded Corners

**File**: Any `.cshtml` page
**Find**: `border-radius:` property
```css
/* BEFORE - very rounded */
border-radius: 20px;

/* AFTER - more rounded */
border-radius: 30px;

/* AFTER - less rounded */
border-radius: 10px;

/* AFTER - sharp corners */
border-radius: 0px;

/* AFTER - perfect circle (for square elements) */
border-radius: 50%;
```

---

### 5. Change Shadows

**File**: Any `.cshtml` page
**Find**: `box-shadow:` property
```css
/* TEMPLATE */
box-shadow:
    horizontal  /* left(-) or right(+) */
    vertical    /* up(-) or down(+) */
    blur        /* how soft */
    color;      /* what color */

/* EXAMPLES */

/* BEFORE - subtle shadow */
box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);

/* AFTER - darker shadow */
box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);

/* AFTER - bigger shadow */
box-shadow: 0 20px 50px rgba(0, 0, 0, 0.15);

/* AFTER - shadow to the right */
box-shadow: 10px 10px 30px rgba(0, 0, 0, 0.1);

/* AFTER - no shadow */
box-shadow: none;
```

---

### 6. Change Font Sizes

**File**: Any `.cshtml` page
**Find**: `font-size:` property
```css
/* Page titles */
font-size: 32px;  /* BEFORE */
font-size: 40px;  /* AFTER - bigger */

/* Normal text */
font-size: 14px;  /* BEFORE */
font-size: 16px;  /* AFTER - bigger */

/* Small text */
font-size: 12px;  /* BEFORE */
font-size: 10px;  /* AFTER - smaller */
```

---

### 7. Change Hover Effects

#### Make Elements Lift Higher on Hover
**File**: Any `.cshtml` page
**Find**: `.element:hover { transform: translateY(-10px); }`
```css
/* BEFORE - lifts 10px */
.stat-card:hover {
    transform: translateY(-10px);
}

/* AFTER - lifts 20px (more dramatic) */
.stat-card:hover {
    transform: translateY(-20px);
}

/* AFTER - lifts 5px (more subtle) */
.stat-card:hover {
    transform: translateY(-5px);
}
```

#### Make Elements Grow More on Hover
**File**: Any `.cshtml` page
**Find**: `.element:hover { transform: scale(1.05); }`
```css
/* BEFORE - grows 5% */
.element:hover {
    transform: scale(1.05);
}

/* AFTER - grows 10% */
.element:hover {
    transform: scale(1.1);
}

/* AFTER - grows 20% */
.element:hover {
    transform: scale(1.2);
}
```

---

### 8. Add/Remove Animations

#### Remove Animation from Element
**File**: Any `.cshtml` page
**Find**: `animation:` property or animation class
```html
<!-- BEFORE - has animation -->
<div class="animate-fade-in">Content</div>

<!-- AFTER - no animation -->
<div>Content</div>
```

#### Add Animation to Element
**File**: Any `.cshtml` page
**Add**: Animation class to HTML
```html
<!-- BEFORE - no animation -->
<div class="container">Content</div>

<!-- AFTER - fades in from bottom -->
<div class="container animate-fade-in-up">Content</div>
```

**Available Animation Classes:**
- `animate-fade-in` - Simple fade in
- `animate-fade-in-up` - Fade in from bottom
- `animate-fade-in-down` - Fade in from top
- `animate-slide-in-left` - Slide from left
- `animate-slide-in-right` - Slide from right
- `animate-scale-in` - Grow from small to normal

**Add Stagger Delay:**
```html
<div class="animate-fade-in stagger-1">First</div>
<div class="animate-fade-in stagger-2">Second</div>
<div class="animate-fade-in stagger-3">Third</div>
```

---

### 9. Change Button Styles

#### Change Button Color
**File**: Any `.cshtml` page
**Find**: Button class
```html
<!-- Purple button -->
<button class="btn btn-modern btn-modern-primary">Click</button>

<!-- Green button -->
<button class="btn btn-modern btn-modern-success">Click</button>

<!-- Red button -->
<button class="btn btn-modern btn-modern-danger">Click</button>

<!-- Blue button -->
<button class="btn btn-modern-info">Click</button>

<!-- Outline button -->
<button class="btn btn-modern btn-modern-outline">Click</button>
```

#### Change Button Size
**File**: Any `.cshtml` page
**Find**: `padding:` in button style
```css
/* BEFORE - normal size */
.btn-modern {
    padding: 12px 28px;
}

/* AFTER - bigger button */
.btn-modern {
    padding: 16px 36px;
}

/* AFTER - smaller button */
.btn-modern {
    padding: 8px 20px;
}
```

**OR use inline style:**
```html
<button class="btn btn-modern" style="padding: 16px 40px;">
    Big Button
</button>
```

---

### 10. Change Table Colors

#### Change Table Header Color
**File**: Any page with a table
**Find**: `thead { background: ... }`
```css
/* BEFORE - purple gradient */
.table-modern thead {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

/* AFTER - blue gradient */
.table-modern thead {
    background: linear-gradient(135deg, #3b82f6 0%, #1d4ed8 100%);
}

/* AFTER - solid color (no gradient) */
.table-modern thead {
    background: #667eea;
}
```

#### Change Row Hover Color
**File**: Any page with a table
**Find**: `tbody tr:hover { background: ... }`
```css
/* BEFORE - light purple on hover */
.table-modern tbody tr:hover {
    background: linear-gradient(90deg, rgba(102, 126, 234, 0.08) 0%, rgba(118, 75, 162, 0.08) 100%);
}

/* AFTER - light blue on hover */
.table-modern tbody tr:hover {
    background: linear-gradient(90deg, rgba(59, 130, 246, 0.08) 0%, rgba(29, 78, 216, 0.08) 100%);
}

/* AFTER - solid light gray */
.table-modern tbody tr:hover {
    background: #f5f5f5;
}
```

---

## 📍 File-Specific Edit Locations

### Global Styles (Affects ALL Pages)

#### Animations (wwwroot/css/animations.css)
- **Change animation speeds**: Lines with `@keyframes` and duration values
- **Add new animations**: Create new `@keyframes` blocks
- **Modify hover effects**: `.hover-lift`, `.hover-glow`, etc.

#### Components (wwwroot/css/components.css)
- **Change button styles**: `.btn-modern-primary`, `.btn-modern-success`, etc.
- **Change card styles**: `.card-modern`, `.card-dashboard`, etc.
- **Change form styles**: `.input-modern`, `.input-enhanced`, etc.
- **Change table styles**: `.table-modern thead`, `.table-modern tbody tr:hover`, etc.

#### JavaScript (wwwroot/js/site.js)
- **Change animation timings**: Numbers in `setTimeout()` calls
- **Enable/disable features**: Comment out functions in `DOMContentLoaded`
- **Modify ripple effect**: `initButtonRipples()` function

### Layout (Affects All Pages)

#### Navbar (_Layout.cshtml)
- **Lines 15-135**: Entire navbar structure
- **Line 18-30**: Logo and brand name
- **Lines 40-111**: Navigation links
- **Lines 120-128**: Logout button

#### Footer (_Layout.cshtml)
- **Lines 142-148**: Footer content

### Page-Specific Styles

#### Login Page (Login.cshtml)
- **Lines 8-229**: All CSS styles for login page
- **Background gradient**: Line 9 (`background: linear-gradient(...)`)
- **Card style**: Lines 25-35
- **Input styles**: Lines 119-135

#### Admin Dashboard (Admins/Index.cshtml)
- **Lines 8-198**: All CSS styles
- **Stat card hover effect**: Lines 80-91
- **Icon styles**: Lines 93-118
- **Gradient colors**: Lines 16, 53, 57, 73, 77, 144, 151

#### Employees Page (Admins/Employees(operations)/Employees.cshtml)
- **Lines 8-142**: All CSS styles
- **Table colors**: Lines 49-50 (thead)
- **Row hover effect**: Lines 78-82

---

## 🎨 Quick Color Reference

### Current Color Scheme

**Primary (Purple):**
- Light: `#667eea`
- Dark: `#764ba2`
- Gradient: `linear-gradient(135deg, #667eea 0%, #764ba2 100%)`

**Success (Green):**
- Light: `#11998e`
- Dark: `#38ef7d`
- Gradient: `linear-gradient(135deg, #11998e 0%, #38ef7d 100%)`

**Danger (Red):**
- Light: `#eb3349`
- Dark: `#f45c43`
- Gradient: `linear-gradient(135deg, #eb3349 0%, #f45c43 100%)`

**Info (Blue/Cyan):**
- Light: `#4facfe`
- Dark: `#00f2fe`
- Gradient: `linear-gradient(135deg, #4facfe 0%, #00f2fe 100%)`

**Pink:**
- Light: `#f093fb`
- Dark: `#f5576c`
- Gradient: `linear-gradient(135deg, #f093fb 0%, #f5576c 100%)`

**Neutrals:**
- Dark gray: `#2c3e50`
- Medium gray: `#7f8c8d`
- Light gray: `#95a5a6`
- Very light gray: `#f0f0f0`

---

## 🔍 Find & Replace Guide

### Change ALL Purple Gradients to Blue

**Find:**
```css
linear-gradient(135deg, #667eea 0%, #764ba2 100%)
```

**Replace with:**
```css
linear-gradient(135deg, #3b82f6 0%, #1d4ed8 100%)
```

**Where:** All `.cshtml` files

### Change ALL Animation Speeds

**Find:** `0.6s`
**Replace with:** `0.4s` (faster) or `0.8s` (slower)
**Where:** All `.cshtml` files and `animations.css`

### Change ALL Border Radius

**Find:** `border-radius: 20px;`
**Replace with:** `border-radius: 15px;` or `border-radius: 25px;`
**Where:** All `.cshtml` files

---

## ⚡ Pro Tips

1. **Test Changes on One Page First**
   - Edit one `.cshtml` file
   - Refresh browser (Ctrl+F5)
   - If it looks good, apply to other pages

2. **Use Browser DevTools**
   - Press F12 in browser
   - Click element you want to change
   - See its CSS in "Styles" panel
   - Edit values live to test
   - Copy working values back to `.cshtml` file

3. **Keep Backups**
   - Before major changes, copy the `.cshtml` file
   - Name it like `Employees.cshtml.backup`
   - You can always revert if needed

4. **Change Gradually**
   - Don't change 10 things at once
   - Change one property at a time
   - Test after each change

5. **Use Comments**
   - Add `/* My change */` before your edits
   - Makes it easy to find your changes later

---

**That's everything! You can now edit any aspect of the UI!** 🎉
