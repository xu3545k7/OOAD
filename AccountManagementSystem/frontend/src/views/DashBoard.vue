<template>
    <div>
        <Header />
        <h1>Welcome, {{ userName }}</h1>
        <p>Your role: {{ role }}</p>

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
            <p>No special privileges assigned.</p>
        </div>
    </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import Header from "@/components/Header.vue";

export default {
    components: { Header },
    computed: {
        ...mapState("account", ["userName", "role"]),
    },
    methods: {
        ...mapActions("account", ["fetchAccountData"]),
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
    mounted() {
        this.fetchAccountData();
    },
};
</script>

<style scoped>
h1,
h2,
p {
    color: #333;
}
</style>
