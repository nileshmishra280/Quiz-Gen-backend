<script>
    import { goto } from '$app/navigation';
    import { fade, fly, scale } from 'svelte/transition';
    import { elasticOut } from 'svelte/easing';
    import { onMount } from 'svelte';

    let isVisible = false;

    onMount(() => {
        isVisible = true;
    });
</script>

<svelte:head>
    <link href="https://cdn.jsdelivr.net/npm/remixicon@3.5.0/fonts/remixicon.css" rel="stylesheet" />
</svelte:head>

<div class="min-h-screen bg-gray-50">
    <header 
        class="sticky top-0 z-50 bg-white/90 backdrop-blur-sm px-4 py-4 shadow-md sm:px-6 lg:px-8"
        in:fly={{y: -20, duration: 800}}
    >
        <div class="mx-auto flex max-w-6xl items-center justify-between">
            <div class="flex items-center transform hover:scale-105 transition-transform duration-300">
                <i class="ri-question-line mr-2 text-3xl text-indigo-600"></i>
                <span class="text-xl font-semibold text-indigo-600">Quizee</span>
            </div>
            <nav class="flex items-center space-x-6">
                {#each ['Features', 'Pricing', 'About'] as item}
                    <a 
                        href="/{item.toLowerCase()}" 
                        class="text-gray-600 hover:text-indigo-600 transition-colors duration-300 relative group"
                    >
                        {item}
                        <span class="absolute bottom-0 left-0 w-0 h-0.5 bg-indigo-600 transition-all duration-300 group-hover:w-full"></span>
                    </a>
                {/each}
                <a
                    href="/auth/login"
                    class="rounded-full bg-indigo-600 px-4 py-2 text-white hover:bg-indigo-700 transform hover:scale-105 transition-all duration-300 shadow-md hover:shadow-lg"
                >
                    Login
                </a>
                <a
                    href="/auth/signup"
                    class="rounded-full bg-gray-200 px-4 py-2 text-gray-700 hover:bg-gray-300 transform hover:scale-105 transition-all duration-300 shadow-md hover:shadow-lg"
                >
                    Sign Up
                </a>
            </nav>
        </div>
    </header>

    <section
        class="bg-gradient-to-r from-indigo-600 to-purple-600 px-4 py-32 text-white sm:px-6 lg:px-8 relative overflow-hidden"
        id="features"
    >
        {#if isVisible}
            <div 
                class="mx-auto max-w-3xl text-center relative z-10"
                in:fly={{y: 20, duration: 1000}}
            >
                <h1 class="mb-4 text-5xl font-bold">Engage, Learn, and Quiz with Quizee</h1>
                <p class="mb-8 text-xl opacity-90">
                    Create and participate in live quizzes powered by <span class="font-semibold">Gemini AI.</span> Generate unique questions
                    and enhance your learning experience.
                </p>
                <a
                    href="auth/signup"
                    class="rounded-full bg-white px-8 py-4 font-semibold text-indigo-600 hover:bg-gray-100 transform hover:scale-105 transition-all duration-300 shadow-lg hover:shadow-xl inline-block"
                >
                    Get Started for Free
                </a>
            </div>
        {/if}
        <!-- Abstract background shapes -->
        <div class="absolute inset-0 opacity-10">
            <div class="absolute top-20 left-20 w-40 h-40 rounded-full bg-white transform rotate-45"></div>
            <div class="absolute bottom-20 right-20 w-60 h-60 rounded-full bg-white"></div>
        </div>
    </section>

    <section class="px-4 py-24 sm:px-6 lg:px-8 bg-gray-50">
        {#if isVisible}
            <div class="mx-auto max-w-6xl text-center">
                <h2 
                    class="mb-12 text-3xl font-semibold text-gray-800"
                    in:fly={{y: 20, duration: 800}}
                >
                    Key Features
                </h2>
                <div class="grid grid-cols-1 gap-8 md:grid-cols-3">
                    {#each [
                        {
                            icon: 'ri-magic-line',
                            title: 'AI-Powered Questions',
                            description: 'Generate unique and engaging quiz questions with the power of Gemini AI.'
                        },
                        {
                            icon: 'ri-live-line',
                            title: 'Live Quiz Experience',
                            description: 'Participate in real-time quizzes with friends and colleagues, making learning fun and interactive.'
                        },
                        {
                            icon: 'ri-bar-chart-line',
                            title: 'Detailed Analytics',
                            description: 'Track your progress and performance with detailed analytics and reports.'
                        }
                    ] as feature, i}
                        <div 
                            class="rounded-xl bg-white p-8 shadow-lg hover:shadow-xl transition-all duration-300 transform hover:-translate-y-1"
                            in:fly={{y: 20, duration: 800, delay: 200 * i}}
                        >
                            <i class="{feature.icon} mb-4 text-4xl text-indigo-600"></i>
                            <h3 class="mb-4 text-xl font-semibold">{feature.title}</h3>
                            <p class="text-gray-600">{feature.description}</p>
                        </div>
                    {/each}
                </div>
            </div>
        {/if}
    </section>

    <section class="bg-gradient-to-br from-indigo-50 to-purple-50 px-4 py-24 sm:px-6 lg:px-8">
        {#if isVisible}
            <div 
                class="mx-auto max-w-3xl text-center"
                in:fly={{y: 20, duration: 800}}
            >
                <h2 class="mb-8 text-4xl font-semibold text-gray-800">Ready to Quiz?</h2>
                <p class="mb-8 text-xl text-gray-600">
                    Join Quizee and experience the future of interactive learning.
                </p>
                <a
                    href="/signup"
                    class="rounded-full bg-indigo-600 px-8 py-4 font-semibold text-white hover:bg-indigo-700 transform hover:scale-105 transition-all duration-300 shadow-lg hover:shadow-xl inline-block"
                >
                    Sign Up Now and Start Quizzing
                </a>
            </div>
        {/if}
    </section>

    <footer 
        class="bg-gray-800 px-4 py-12 text-white sm:px-6 lg:px-8"
        in:fly={{y: 20, duration: 800}}
    >
        <div class="mx-auto max-w-6xl text-center">
            <nav class="mb-6">
                {#each ['Privacy Policy', 'Terms of Service', 'Contact Us'] as item}
                    <a 
                        href="/contact" 
                        class="mx-4 text-gray-300 hover:text-white transition-colors duration-300 relative group"
                    >
                        {item}
                        <span class="absolute bottom-0 left-0 w-0 h-0.5 bg-white transition-all duration-300 group-hover:w-full"></span>
                    </a>
                {/each}
            </nav>
            <p class="text-gray-400">&copy; {new Date().getFullYear()} Quizee. All rights reserved.</p>
        </div>
    </footer>
</div>

<style>
    :global(body) {
        overflow-x: hidden;
    }
</style>