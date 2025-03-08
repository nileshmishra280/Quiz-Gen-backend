<script>
    import { page } from '$app/stores';
    import { Navbar, NavBrand, NavLi, NavUl, NavHamburger } from 'flowbite-svelte';
    import { goto } from '$app/navigation';
    $: activeUrl = $page.url.pathname;
  </script>
  
  <Navbar>
    <NavBrand href="/">
      <img src="https://www.svgrepo.com/show/530399/inspiration.svg" class="me-3 h-6 sm:h-9" alt="Flowbite Logo" />
      <span class="self-center whitespace-nowrap text-xl font-semibold dark:text-white">Flowbite</span>
    </NavBrand>
    <NavHamburger />
    <NavUl {activeUrl}>
      <NavLi href="/Auth/login">Login</NavLi>
      <NavLi href="#">Navbar</NavLi>
      <NavLi href="#">Accordion</NavLi>
      <NavLi href="#">Alert</NavLi>
      <NavLi href="#">Avatar</NavLi>
    </NavUl>
  </Navbar>