import { UnauthorizedException } from "@nestjs/common";
import { PassportStrategy } from "@nestjs/passport";
import { ExtractJwt, Strategy } from "passport-jwt";

export class JwtStrategy extends PassportStrategy(Strategy, 'jwt') {
    constructor() {
        super({
            jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
            ignoreExpiration: false,
            secretOrKey: process.env.JWT_SECRET || "default_secret_key"
        });

        // console.log("✅ JwtStrategy Loaded!"); 
    }

    async validate(payload: any) {
        // console.log("✅ JWT Payload:", payload); 

        if (!payload || !payload.sub) {
            throw new UnauthorizedException("Invalid token payload.");
        }

        const user = { id: payload.sub, email: payload.username };
        // console.log("✅ Returning user:", user);

        return user;
    }
}
