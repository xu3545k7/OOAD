<template>
  <div id="app">
    <header class="header">
      <h1>Account Management System</h1>
      <nav>
        <button v-if="!isLoggedIn" @click="currentPage = 'login'">Login</button>
        <button v-if="!isLoggedIn" @click="currentPage = 'register'">Register</button>
        <button v-if="isLoggedIn" @click="currentPage = 'dashboard'">Dashboard</button>
        <button v-if="isLoggedIn" @click="logout">Logout</button>
      </nav>
    </header>

    <main class="content">
      <UserLogin v-if="currentPage === 'login'" @loginSuccess="onLoginSuccess" />
      <UserRegister v-if="currentPage === 'register'" />
      <UserDashboard v-if="currentPage === 'dashboard'" />
    </main>

    <footer class="footer">
      <p>&copy; 2025 Account Management System. All Rights Reserved.</p>
    </footer>
  </div>
</template>

<script>
import UserLogin from "@/components/UserLogin.vue";
import UserRegister from "@/components/UserRegister.vue";
import UserDashboard from "@/components/UserDashboard.vue";

export default {
  name: "App",
  components: {
    UserLogin,
    UserRegister,
    UserDashboard,
  },
  data() {
    return {
      currentPage: "login", // Default page is login
      isLoggedIn: false,
    };
  },
  methods: {
    onLoginSuccess() {
      this.isLoggedIn = true;
      this.currentPage = "dashboard";
    },
    logout() {
      this.isLoggedIn = false;
      this.currentPage = "login";
    },
  },
};
</script>

<style>
#app {
  display: flex;
  flex-direction: column;
  height: 100vh;
}

.header {
  background-color: #4CAF50;
  color: white;
  padding: 10px;
  text-align: center;
}

.header nav {
  margin-top: 10px;
}

.header button {
  background: white;
  border: none;
  padding: 10px 20px;
  margin: 0 5px;
  border-radius: 5px;
  cursor: pointer;
}

.header button:hover {
  background: #45a049;
  color: white;
}

.content {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 20px;
}

.footer {
  background-color: #4CAF50;
  color: white;
  text-align: center;
  padding: 10px;
}
</style>
