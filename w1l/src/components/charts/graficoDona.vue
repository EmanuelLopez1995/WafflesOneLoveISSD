<template>
    <Doughnut :data="chartData" :options="chartOptions" />
  </template>
  
  <script setup lang="ts">
  import { defineProps } from 'vue';
  import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
  import { Doughnut } from 'vue-chartjs';
  
  ChartJS.register(ArcElement, Tooltip, Legend);

  const chartOptions = {
  responsive: true,
  maintainAspectRatio: false
}
  
  const props = defineProps<{
    chartData: any;
  }>();
  </script>