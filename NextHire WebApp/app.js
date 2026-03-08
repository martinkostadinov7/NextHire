// CHANGE THIS to your API's actual URL (e.g., http://localhost:5000/api)
const API_BASE_URL = 'https://localhost:7001/api'; 

let currentEntity = 'Users';

// Schema mapping based on your Create/Update DTOs
const entitySchemas = {
    Users: [
        { name: 'email', label: 'Email', type: 'email' },
        { name: 'firstName', label: 'First Name', type: 'text' },
        { name: 'lastName', label: 'Last Name', type: 'text' },
        { name: 'phone', label: 'Phone', type: 'text' }
    ],
    Companies: [
        { name: 'name', label: 'Company Name', type: 'text' },
        { name: 'description', label: 'Description', type: 'text' }
    ],
    Offers: [
        { name: 'title', label: 'Title', type: 'text' },
        { name: 'description', label: 'Description', type: 'text' },
        { name: 'companyId', label: 'Company ID', type: 'number' }
    ],
    Cvs: [
        { name: 'summary', label: 'Summary', type: 'text' },
        { name: 'experience', label: 'Experience', type: 'text' },
        { name: 'skills', label: 'Skills', type: 'text' },
        { name: 'userId', label: 'User ID', type: 'number' }
    ],
    Applications: [
        { name: 'email', label: 'Email', type: 'email' },
        { name: 'firstName', label: 'First Name', type: 'text' },
        { name: 'lastName', label: 'Last Name', type: 'text' },
        { name: 'phone', label: 'Phone', type: 'text' },
        { name: 'education', label: 'Education', type: 'text' },
        { name: 'coverLetter', label: 'Cover Letter', type: 'text' },
        { name: 'cvId', label: 'CV ID', type: 'number' },
        { name: 'userId', label: 'User ID', type: 'number' },
        { name: 'offerId', label: 'Offer ID', type: 'number' },
        { name: 'submittedAt', label: 'Submitted At', type: 'datetime-local' }
    ]
};

// DOM Elements
const navMenu = document.getElementById('nav-menu');
const entityTitle = document.getElementById('entity-title');
const tableHead = document.getElementById('table-head');
const tableBody = document.getElementById('table-body');
const addBtn = document.getElementById('add-btn');
const modal = document.getElementById('form-modal');
const closeModal = document.getElementById('close-modal');
const entityForm = document.getElementById('entity-form');
const formFields = document.getElementById('form-fields');
const modalTitle = document.getElementById('modal-title');
const entityIdInput = document.getElementById('entity-id');

// Event Listeners
navMenu.addEventListener('click', (e) => {
    if (e.target.tagName === 'BUTTON') {
        document.querySelectorAll('.nav-btn').forEach(btn => btn.classList.remove('active'));
        e.target.classList.add('active');
        currentEntity = e.target.getAttribute('data-entity');
        entityTitle.innerText = currentEntity;
        loadData();
    }
});

addBtn.addEventListener('click', () => openModal());
closeModal.addEventListener('click', () => modal.classList.add('hidden'));
entityForm.addEventListener('submit', handleFormSubmit);

// API Functions
async function loadData() {
    try {
        const response = await fetch(`${API_BASE_URL}/${currentEntity}`);
        if (!response.ok) throw new Error('Network response was not ok');
        const data = await response.json();
        renderTable(data);
    } catch (error) {
        console.error("Error fetching data:", error);
        tableBody.innerHTML = `<tr><td colspan="100%">Failed to load data. Is the backend running?</td></tr>`;
    }
}

async function handleFormSubmit(e) {
    e.preventDefault();
    const id = entityIdInput.value;
    const method = id ? 'PUT' : 'POST';
    const url = id ? `${API_BASE_URL}/${currentEntity}/${id}` : `${API_BASE_URL}/${currentEntity}`;
    
    // Construct payload dynamically based on form inputs
    const payload = {};
    const formData = new FormData(entityForm);
    entitySchemas[currentEntity].forEach(field => {
        let val = formData.get(field.name);
        if (field.type === 'number') val = parseInt(val) || 0;
        payload[field.name] = val;
    });

    // C# specific property handling - DTOs usually expect PascalCase or camelCase depending on JSON config
    // We assume default ASP.NET Core JSON serialization (camelCase).

    try {
        const response = await fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (response.ok) {
            modal.classList.add('hidden');
            loadData();
        } else {
            alert('Error saving data');
        }
    } catch (error) {
        console.error("Error saving data:", error);
    }
}

async function deleteItem(id) {
    if (!confirm('Are you sure you want to delete this item?')) return;
    try {
        const response = await fetch(`${API_BASE_URL}/${currentEntity}/${id}`, { method: 'DELETE' });
        if (response.ok) loadData();
        else alert('Error deleting item');
    } catch (error) {
        console.error("Error deleting data:", error);
    }
}

// UI Rendering Functions
function renderTable(data) {
    tableHead.innerHTML = '';
    tableBody.innerHTML = '';

    if (data.length === 0) {
        tableBody.innerHTML = `<tr><td colspan="100%">No records found.</td></tr>`;
        return;
    }

    // Generate Headers based on the first item's keys (ReadDto shape)
    // We filter out complex objects (like nested lists) for table view simplicity
    const sampleItem = data[0];
    const keys = Object.keys(sampleItem).filter(k => typeof sampleItem[k] !== 'object' || sampleItem[k] === null);
    
    let headerRow = '<tr>';
    keys.forEach(key => { headerRow += `<th>${key.charAt(0).toUpperCase() + key.slice(1)}</th>`; });
    headerRow += '<th>Actions</th></tr>';
    tableHead.innerHTML = headerRow;

    // Generate Rows
    data.forEach(item => {
        let row = '<tr>';
        keys.forEach(key => { row += `<td>${item[key] || ''}</td>`; });
        row += `
            <td>
                <button class="edit-btn" onclick='editItem(${JSON.stringify(item).replace(/'/g, "&#39;")})'>Edit</button>
                <button class="danger-btn" onclick="deleteItem(${item.id})">Delete</button>
            </td>
        `;
        row += '</tr>';
        tableBody.insertAdjacentHTML('beforeend', row);
    });
}

function openModal(item = null) {
    modalTitle.innerText = item ? `Edit ${currentEntity.slice(0, -1)}` : `Add ${currentEntity.slice(0, -1)}`;
    entityIdInput.value = item ? item.id : '';
    formFields.innerHTML = '';

    // Generate form fields based on the DTO schema
    entitySchemas[currentEntity].forEach(field => {
        let value = '';
        if (item && item[field.name] !== undefined) {
            value = item[field.name];
            // Fix datetime format for input
            if (field.type === 'datetime-local' && value) {
                value = new Date(value).toISOString().slice(0, 16);
            }
        }

        formFields.innerHTML += `
            <div class="form-group">
                <label>${field.label}</label>
                <input type="${field.type}" name="${field.name}" value="${value}" ${field.type === 'number' ? 'step="1"' : ''} required>
            </div>
        `;
    });

    modal.classList.remove('hidden');
}

// Attach edit function to window so the inline onclick works
window.editItem = function(item) {
    openModal(item);
};

// Initial Load
loadData();