<template>
    <div>
        <h1>Dashboard</h1>
        <div v-if="role === 'PM'">
            <h2>Project Manager Functions</h2>
            <button @click="viewReports">View Reports</button>
            <button @click="manageProjects">Manage Projects</button>
        </div>
        <div v-else-if="role === 'EM'">
            <h2>Employee Manager Functions</h2>
            <button @click="viewEmployees">View Employees</button>
            <button @click="assignTasks">Assign Tasks</button>
        </div>
        <div v-else>
            <h2>General User</h2>
            <p>Welcome to the system.</p>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            role: "", // User role, fetched from backend or local storage
        };
    },
    mounted() {
        // Mock fetching role (replace with actual API call)
        this.role = localStorage.getItem("userRole") || "PM";
    },
    methods: {
        viewReports() {
            alert("Viewing reports...");
        },
        manageProjects() {
            alert("Managing projects...");
        },
        viewEmployees() {
            alert("Viewing employees...");
        },
        assignTasks() {
            alert("Assigning tasks...");
        },
    },
};
</script>
