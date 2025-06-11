<template>
    <div>
        <h1>Register</h1>
        <input v-model="username" placeholder="Username" />
        <input v-model="password" type="password" placeholder="Password" />
        <select v-model="role">
            <option value="PM">Project Manager</option>
            <option value="EM">Employee Manager</option>
        </select>
        <button @click="register">Register</button>
    </div>
</template>

<script>
import { register } from "@/services/api";

export default {
    data() {
        return {
            username: "",
            password: "",
            role: "PM", // Default role
        };
    },
    methods: {
        async register() {
            try {
                await register(this.username, this.password, this.role);
                alert("Registration successful!");
                this.$router.push("/login"); // Redirect to login page
            } catch (error) {
                alert("Registration failed.");
            }
        },
    },
};
</script>
