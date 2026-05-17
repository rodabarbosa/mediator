const { chromium } = require('playwright');
const path = require('path');
const fs = require('fs');

/**
 * Script genérico para capturar mockups em HTML usando Playwright
 * Uso: node capture-mockup.cjs <caminho-html-entrada> <caminho-png-saida>
 */
async function capture() {
  const args = process.argv.slice(2);
  if (args.length !== 2) {
    console.error('ERRO: Parâmetros incorretos.\nUso: node capture-mockup.cjs <caminho-html-entrada> <caminho-png-saida>');
    process.exit(1);
  }

  const inputHtml = path.resolve(args[0]);
  const outputPng = path.resolve(args[1]);

  if (!fs.existsSync(inputHtml)) {
    console.error(`ERRO: Arquivo HTML de entrada não encontrado: ${inputHtml}`);
    process.exit(1);
  }

  // Garantir que a pasta de saída existe
  const outputDir = path.dirname(outputPng);
  if (!fs.existsSync(outputDir)) {
    fs.mkdirSync(outputDir, { recursive: true });
  }

  console.log(`Carregando: file://${inputHtml}`);
  const browser = await chromium.launch({ headless: true });
  const page = await browser.newPage();

  // Resolução HD padrão para boa visibilidade
  await page.setViewportSize({ width: 1280, height: 720 });
  await page.goto(`file://${inputHtml}`);

  // Tirar a screenshot
  await page.screenshot({ path: outputPng });
  await browser.close();

  console.log(`Mockup visual gerado com sucesso em: ${outputPng}`);
}

capture().catch(err => {
  console.error('Erro ao gerar o wireframe:', err);
  process.exit(1);
});
