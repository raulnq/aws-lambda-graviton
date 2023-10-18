﻿import http from 'k6/http';
import { check, sleep } from 'k6';
export const options = {
    stages: [
        { duration: '5m', target: 1 },
    ],
};
export default function () {
    const armpiapi = '<YOUR_URL>/Prod/piarm';
    const x86piapi = '<YOUR_URL>/Prod/pix86';
    const armencryptapi = '<YOUR_URL>/Prod/encryptarm';
    const x86encryptapi = '<YOUR_URL>/Prod/encryptx86';

    const res1 = http.get(armpiapi);
    check(res1, {
        'is status 200 (PI ARM)': (r) => r.status === 200,
    });
    sleep(1);
    const res2 = http.get(x86piapi);
    check(res2, {
        'is status 200 (PI X86)': (r) => r.status === 200,
    });
    sleep(1);
    const res3 = http.get(armencryptapi);
    check(res3, {
        'is status 200 (ENCRYPT ARM)': (r) => r.status === 200,
    });
    sleep(1);
    const res4 = http.get(x86encryptapi);
    check(res4, {
        'is status 200 (ENCRYPT X86)': (r) => r.status === 200,
    });
    sleep(1);
}