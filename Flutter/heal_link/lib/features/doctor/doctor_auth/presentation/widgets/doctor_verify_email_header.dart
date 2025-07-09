
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/widgets/auth_custom_header.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorVerifyEmailHeader extends StatelessWidget {
  const DoctorVerifyEmailHeader({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: AuthCustomHeader(headerTitle: S.of(context).verify_email),
    );
  }
}

