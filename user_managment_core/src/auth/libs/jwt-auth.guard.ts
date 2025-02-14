import { Injectable, CanActivate, ExecutionContext, UnauthorizedException } from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';
import { User } from 'src/registracion/entities/registracion.entity';
import { EntityManager } from 'typeorm';

@Injectable()
export class JwtAuthGuard extends AuthGuard('jwt') implements CanActivate {
  constructor(
    private readonly entityManager: EntityManager
  ) {
    super();
  }

  async canActivate(context: ExecutionContext): Promise<boolean> {
    const request = context.switchToHttp().getRequest();

    const canActivate = await super.canActivate(context);
 
    if (!canActivate || !request.user) {
        throw new UnauthorizedException('User not authorized or token is invalid');
    }

    return true;
}

}
