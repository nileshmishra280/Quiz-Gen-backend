<script>
    import { fade, fly, scale } from 'svelte/transition';
    import { elasticOut } from 'svelte/easing';

    let email = '';
    let password = '';
    let isVisible = true;

    function handleSubmit() {
        console.log('Email:', email, 'Password:', password);
    }
</script>
<svelte:head>
	<link href="https://cdn.jsdelivr.net/npm/remixicon@3.5.0/fonts/remixicon.css" rel="stylesheet" />
</svelte:head>

<div class="min-h-screen flex items-center justify-center bg-gradient-to-br from-indigo-100 to-purple-100">
    <div 
        in:fly={{y: 20, duration: 800, delay: 200}}
        out:fade
        class="bg-white p-8 rounded-lg shadow-xl w-full max-w-sm transform hover:scale-[1.01] transition-transform duration-300"
    >
        <div in:scale={{duration: 400, easing: elasticOut}}>
            <h2 class="text-2xl font-semibold mb-6 text-center text-indigo-700">
                <i class="ri-question-line mr-2 text-3xl text-indigo-600"></i>
                Login to Quizee
            </h2>
        </div>

        <form on:submit|preventDefault={handleSubmit} class="space-y-4">
            {#key isVisible}
                <div 
                    in:fly={{x: -20, duration: 300, delay: 300}}
                    class="form-group"
                >
                    <label for="email" class="block text-gray-700 text-sm font-medium mb-2">Email</label>
                    <div class="relative">
                        <i class="ri-mail-line absolute left-3 top-3 text-gray-400"></i>
                        <input
                            type="email"
                            id="email"
                            bind:value={email}
                            class="pl-10 w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition-all duration-300"
                            placeholder="Enter your email"
                        />
                    </div>
                </div>

                <div 
                    in:fly={{x: -20, duration: 300, delay: 400}}
                    class="form-group"
                >
                    <label for="password" class="block text-gray-700 text-sm font-medium mb-2">Password</label>
                    <div class="relative">
                        <i class="ri-lock-line absolute left-3 top-3 text-gray-400"></i>
                        <input
                            type="password"
                            id="password"
                            bind:value={password}
                            class="pl-10 w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition-all duration-300"
                            placeholder="Enter your password"
                        />
                    </div>
                </div>
            {/key}

            <div 
                in:fly={{y: 20, duration: 300, delay: 500}}
                class="flex items-center justify-between mt-6"
            >
                <button
                    type="submit"
                    class="bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-2 px-6 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transform hover:scale-105 transition-all duration-300"
                >
                    Login
                </button>
                <a 
                    href="/auth/signup" 
                    class="inline-block align-baseline font-bold text-sm text-indigo-500 hover:text-indigo-800 transform hover:scale-105 transition-all duration-300"
                >
                    Sign Up
                </a>
            </div>

            <div 
                in:fly={{y: 20, duration: 300, delay: 600}}
                class="mt-4 text-center"
            >
                <a 
                    href="/auth/forgot-password" 
                    class="text-sm text-gray-600 hover:text-indigo-600 transition-colors duration-300"
                >
                    Forgot your password?
                </a>
            </div>
        </form>
    </div>
</div>

<style>
    :global(body) {
        overflow-x: hidden;
    }

    .form-group {
        opacity: 0;
        animation: fadeIn 0.5s forwards;
    }

    @keyframes fadeIn {
        to {
            opacity: 1;
        }
    }
</style>