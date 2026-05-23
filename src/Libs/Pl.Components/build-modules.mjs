import glob from 'fast-glob'
import { build } from 'esbuild'

await build({
    entryPoints: await glob("wwwroot/ts/modules/**/*.ts"),
    outdir: "wwwroot/js/",
    format: "esm",
    target: "es2020",
    bundle: false,
    minify: true
});