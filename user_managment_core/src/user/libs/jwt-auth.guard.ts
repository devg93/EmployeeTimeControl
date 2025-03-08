import { Injectable, CanActivate, ExecutionContext, UnauthorizedException } from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';


@Injectable()
export class JwtAuthGuard extends AuthGuard('jwt') implements CanActivate {
  constructor() {
    super();
  }

  async canActivate(context: ExecutionContext): Promise<boolean> {
    const request = context.switchToHttp().getRequest();
    
    // console.log('🔹 Incoming Request Headers:', request.headers);
    // console.log('🔹 Incoming Request Authorization:', request.headers.authorization);
    // console.log('🔹 Decoded User:', request.user);
    
    const canActivate = await super.canActivate(context);

    if (!canActivate || !request.user) {
        throw new UnauthorizedException('User not authorized or token is invalid');
    }

    return true;
}


}
