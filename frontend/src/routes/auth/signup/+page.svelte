<script>
	import { fade, fly, scale } from 'svelte/transition';
	import { elasticOut } from 'svelte/easing';

	let name = '';
	let email = '';
	let password = '';
	let confirmPassword = '';
	let errorMessage = '';
	let isVisible = true; // For animation control

	function handleSubmit() {
		if (password !== confirmPassword) {
			errorMessage = 'Passwords do not match.';
			return;
		}

		console.log('Name:', name, 'Email:', email, 'Password:', password);
		errorMessage = '';
	}
</script>

<svelte:head>
	<link href="https://cdn.jsdelivr.net/npm/remixicon@3.5.0/fonts/remixicon.css" rel="stylesheet" />
</svelte:head>

<div
	class="flex min-h-screen items-center justify-center bg-gradient-to-br from-indigo-100 to-purple-100"
>
	<div
		in:fly={{ y: 20, duration: 800, delay: 200 }}
		out:fade
		class="w-full max-w-sm transform rounded-lg bg-white p-8 shadow-xl transition-transform duration-300 hover:scale-[1.01]"
	>
		<div in:scale={{ duration: 400, easing: elasticOut }}>
			<h2 class="mb-6 text-center text-2xl font-semibold text-indigo-700">
				<i class="ri-question-line mr-2 text-3xl text-indigo-600"></i>
				Sign Up for Quizee
			</h2>
		</div>

		{#if errorMessage}
			<div
				in:fly={{ y: -20, duration: 300 }}
				out:fade={{ duration: 200 }}
				class="mb-4 rounded border-l-4 border-red-500 bg-red-100 p-4 text-red-700"
			>
				<p class="text-sm">{errorMessage}</p>
			</div>
		{/if}

		<form on:submit|preventDefault={handleSubmit} class="space-y-4">
			{#key isVisible}
				<div in:fly={{ x: -20, duration: 300, delay: 300 }} class="form-group">
					<label for="name" class="mb-2 block text-sm font-medium text-gray-700">Name</label>
					<div class="relative">
						<i class="ri-user-line absolute top-3 left-3 text-gray-400"></i>
						<input
							type="text"
							id="name"
							bind:value={name}
							class="w-full rounded-md border border-gray-300 px-3 py-2 pl-10 transition-all duration-300 focus:border-transparent focus:ring-2 focus:ring-indigo-500"
							placeholder="Enter your name"
						/>
					</div>
				</div>

				<div in:fly={{ x: -20, duration: 300, delay: 400 }} class="form-group">
					<label for="email" class="mb-2 block text-sm font-medium text-gray-700">Email</label>
					<div class="relative">
						<i class="ri-mail-line absolute top-3 left-3 text-gray-400"></i>
						<input
							type="email"
							id="email"
							bind:value={email}
							class="w-full rounded-md border border-gray-300 px-3 py-2 pl-10 transition-all duration-300 focus:border-transparent focus:ring-2 focus:ring-indigo-500"
							placeholder="Enter your email"
						/>
					</div>
				</div>

				<div in:fly={{ x: -20, duration: 300, delay: 500 }} class="form-group">
					<label for="password" class="mb-2 block text-sm font-medium text-gray-700">Password</label
					>
					<div class="relative">
						<i class="ri-lock-line absolute top-3 left-3 text-gray-400"></i>
						<input
							type="password"
							id="password"
							bind:value={password}
							class="w-full rounded-md border border-gray-300 px-3 py-2 pl-10 transition-all duration-300 focus:border-transparent focus:ring-2 focus:ring-indigo-500"
							placeholder="Enter your password"
						/>
					</div>
				</div>

				<div in:fly={{ x: -20, duration: 300, delay: 600 }} class="form-group">
					<label for="confirmPassword" class="mb-2 block text-sm font-medium text-gray-700"
						>Confirm Password</label
					>
					<div class="relative">
						<i class="ri-lock-line absolute top-3 left-3 text-gray-400"></i>
						<input
							type="password"
							id="confirmPassword"
							bind:value={confirmPassword}
							class="w-full rounded-md border border-gray-300 px-3 py-2 pl-10 transition-all duration-300 focus:border-transparent focus:ring-2 focus:ring-indigo-500"
							placeholder="Confirm your password"
						/>
					</div>
				</div>
			{/key}

			<div
				in:fly={{ y: 20, duration: 300, delay: 700 }}
				class="mt-6 flex items-center justify-between"
			>
				<button
					type="submit"
					class="transform rounded-md bg-indigo-600 px-6 py-2 font-bold text-white transition-all duration-300 hover:scale-105 hover:bg-indigo-700 focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 focus:outline-none"
				>
					Sign Up
				</button>
				<a
					href="/auth/login"
					class="inline-block transform align-baseline text-sm font-bold text-indigo-500 transition-all duration-300 hover:scale-105 hover:text-indigo-800"
				>
					Login
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
