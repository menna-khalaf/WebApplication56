
let quantities = {
    'افلام اشعه': 0,
    'افارول': 0,
    'افلام بانوراما': 0,
    'اكياس حمراء': 0,
    'اكياس صفراء': 0,
    'انابيب حنجريه': 0,
    'اوفر هيد': 0,
    'باونش': 0,
    'بلاستر طبي': 0,
    'بنج': 0,
    'بيتادين': 0,
    'جهاز اعطاء محلول': 0,
    'جوانتي لاتكس': 0,
    'جوانتي معقم': 0,
    'جون عزل': 0,
    'جون معقم': 0,
    'حامل استرليام': 0,
    'حامل صابون': 0,
    'حمض مثبت': 0,
    'حمض مطهر': 0,
    'خافض لسان': 0,
    'خرطوم شفط': 0,
    'خيط حرير': 0,
    'راب': 0,
    'رباط شاش': 0,
    'سرنجه 10 سم': 0,
    'سرنجه 3 سم': 0,
    'سرنجه 5 سم': 0,
    'سفت بوكس': 0,
    'سن مشرط': 0,
    'شاش سواب': 0,
    'شاش طبي': 0,
    'شرائح زجاج': 0,
    'شرائح تعقيم': 0,
    'شرائط سكر': 0,
    'شفاط لعاب': 0,
    'شكاكات سكر': 0,
    "صابون ايدي": 0,
    "عازل دياثرم": 0,
    "فرش تنظيف": 0,
    "فيس شيلد": 0,
    "قسطره شفط": 0,
    "قطن طبي": 0,
    "كانيولات": 0,
    "الكتروت رسم قلب": 0,
    "كحول": 0,
    "كلور": 0,
    "كوب بلاستيك": 0,
    "كيس جمع بول": 0,
    "ماء اوكسجين": 0,
    "ماسك ان 95": 0,
    "ماسك عالي الكفائه": 0,
    "ماسك ورقي": 0,
    "محلول ملح": 0,
    "مصل انفلونزا": 0,
    "مضخه استرليم": 0,
    "مطهر ايدي": 0,
    "مناديل ورقيه": 0,
    "منظف انزيمي": 0,
    "نابكن": 0,
    "نظاره": 0,
    "نيدل": 0,
    "وصلات تخدير": 0,
    "يونسيتا": 0,
};


function searchElements() {
    let searchTerm = document.getElementById('searchInput').value.toLowerCase();

    document.querySelectorAll('.element').forEach(element => {
        let elementName = element.id.toLowerCase();
        if (elementName.includes(searchTerm)) {
            element.style.display = 'inline-block';
        } else {
            element.style.display = 'none';
        }
    });
}

function increaseQuantity(element) {
    let inputField = document.getElementById(`${element}-quantity`);
    let newValue = parseInt(inputField.value) + 1;
    inputField.value = newValue;
    updateQuantity(element, newValue);
}

function decreaseQuantity(element) {
    let inputField = document.getElementById(`${element}-quantity`);
    let newValue = parseInt(inputField.value) - 1;
    if (newValue < 0) newValue = 0;
    inputField.value = newValue;
    updateQuantity(element, newValue);
}

function updateQuantity(element, value) {
    let newValue = parseInt(value);
    if (!isNaN(newValue)) {
        quantities[element] = newValue;
    } else {
        quantities[element] = 0;
    }
}


